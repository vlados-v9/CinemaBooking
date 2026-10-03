using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;
using CinemaBooking.Domain.Exceptions;
using CinemaBooking.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Domain.Providers.Implementation;

internal class BookingProvider(ICinemaBookingContext context,
    IContiguousSeatFinder contiguousSeatFinder) : IBookingProvider
{
    public async Task ConfirmBooking(Guid bookingId, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings.SingleOrDefaultAsync(b => b.Id == bookingId, cancellationToken)
            ?? throw new NotFoundException($"Booking with id {bookingId} not found.");

        var seats = await context.Seats.Where(s => booking.ReservedSeatIds.Contains(s.Id)).ToListAsync(cancellationToken);

        var dateTimeUtcNow = DateTime.UtcNow;

        var areNoRejectedSeats = seats.All(seat =>
                                      seat.Status == (short)SeatStatus.Reserved &&
                                      DateTime.FromBinary(seat.ReservationTime!.Value).AddMinutes(10) > dateTimeUtcNow);

        try
        {
            if (!areNoRejectedSeats)
            {
                throw new InvalidOperationException("One or more seats are not reserved for booking or have expired.");
            }

            foreach (var seat in seats)
            {
                seat.UpdateStatus(SeatStatus.Sold, dateTimeUtcNow);
            }
        }
        catch (Exception)
        {
            foreach (var seat in seats)
            {
                seat.UpdateStatus(SeatStatus.Available, dateTimeUtcNow);
            }

            throw;
        }
        finally
        {
            await context.SaveChanges(cancellationToken);
        }
    }

    public async Task<BookingResponse> CreateBooking(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        //TODO: Validate the booking request

        var showtime = await context.Showtimes
            .Include(s => s.Seats)
            .Include(s => s.Movie)
            .SingleOrDefaultAsync(st => st.Id == bookingRequest.ShowTimeId, cancellationToken)
            ?? throw new NotFoundException($"Showtime with id {bookingRequest.ShowTimeId} not found.");

        var availableSeats = showtime.Seats.Where(s => bookingRequest.SeatIds.Contains(s.Id) &&
                                                        s.Status == (short)SeatStatus.Available).ToList();

        if (availableSeats.Count != bookingRequest.SeatIds.Distinct().Count())
        {
            throw new InvalidOperationException("One or more requested seats are not available.");
        }

        var dateTimeNow = DateTime.UtcNow;

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            ShowTimeId = bookingRequest.ShowTimeId,
            ReservedSeatIds = availableSeats.Select(s => s.Id).ToList(),
            BookingTime = dateTimeNow
        };

        foreach (var seat in availableSeats)
        {
            seat.UpdateStatus(SeatStatus.Reserved, dateTimeNow);
        }

        await context.Bookings.AddAsync(booking, cancellationToken);
        await context.SaveChanges(cancellationToken);

        var response = CreateBookingResponse(booking.Id, showtime, availableSeats);

        return response;
    }

    public async Task<BookingResponse> CreateBookingForContiguous(int count, long showTimeId, CancellationToken cancellationToken)
    {
        var showtime = await context.Showtimes
            .Include(s => s.Seats)
            .Include(s => s.Movie)
            .SingleOrDefaultAsync(st => st.Id == showTimeId, cancellationToken)
            ?? throw new NotFoundException($"Showtime with id {showTimeId} not found.");

        var contiguousSeats = contiguousSeatFinder.FindContiguousSeats(showtime.Seats, count);

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            ShowTimeId = showTimeId,
            ReservedSeatIds = contiguousSeats.Select(s => s.Id).ToList(),
            BookingTime = DateTimeOffset.UtcNow
        };

        foreach (var seat in contiguousSeats)
        {
            seat.UpdateStatus(SeatStatus.Reserved, DateTime.UtcNow);
        }

        await context.Bookings.AddAsync(booking, cancellationToken);
        await context.SaveChanges(cancellationToken);

        var response = CreateBookingResponse(booking.Id, showtime, contiguousSeats);

        return response;
    }

    public async Task DeleteBooking(Guid bookingId, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings.SingleOrDefaultAsync(b => b.Id == bookingId, cancellationToken)
            ?? throw new NotFoundException($"Booking with id {bookingId} not found.");

        var seats = await context.Seats.Where(s => booking.ReservedSeatIds.Contains(s.Id)).ToListAsync(cancellationToken);

        foreach (var seat in seats)
        {
            seat.UpdateStatus(SeatStatus.Available, DateTime.UtcNow);
        }

        context.Bookings.Remove(booking);
        await context.SaveChanges(cancellationToken);
    }

    public async Task<List<Booking>> GetAllBooking(CancellationToken cancellationToken) =>
        await context.Bookings.AsNoTracking().ToListAsync(cancellationToken);

    public async Task UpdateBooking(BookingUpdateRequest bookingRequest, CancellationToken cancellationToken)
    {
        var booking = await context.Bookings.SingleOrDefaultAsync(b => b.Id == bookingRequest.Id, cancellationToken)
            ?? throw new NotFoundException($"Booking with id {bookingRequest.Id} not found.");

        var returnSeats = await context.Seats.Where(s => booking.ReservedSeatIds.Contains(s.Id)).ToListAsync(cancellationToken);

        foreach (var seat in returnSeats)
        {
            seat.UpdateStatus(SeatStatus.Available, DateTime.UtcNow);
        }

        var dateTimeNow = DateTimeOffset.UtcNow;
        var newBooking = new Booking
        {
            ShowTimeId = bookingRequest.ShowTimeId,
            ReservedSeatIds = bookingRequest.SeatIds.ToList(),
            BookingTime = dateTimeNow
        };

        booking.Update(newBooking);

        var newSeats = await context.Seats.Where(s => bookingRequest.SeatIds.Contains(s.Id)).ToListAsync(cancellationToken);

        foreach (var seat in newSeats)
        {
            seat.UpdateStatus(SeatStatus.Reserved, DateTime.UtcNow);
        }

        await context.SaveChanges(cancellationToken);
    }

    private BookingResponse CreateBookingResponse(Guid bookingId, Showtime showTime, List<Seat> seats)
    {
        var response = new BookingResponse
        {
            BookingId = bookingId,
            ShowtimeInfo = $"Showtime info: Start - {showTime.StartTime} / Auditorium - {showTime.AuditoriumId}",
            MovieInfo = $"Movie info: Title - {showTime.Movie.Title} / Year - {showTime.Movie.Year}",
            SeatsInfo = seats.GroupBy(s => s.Row)
           .Select(seat => $"Seats info: Row - {seat.Key} / Number -> {string.Join(", ", seat.Select(s => s.Number))}")
        };

        return response;
    }
}
