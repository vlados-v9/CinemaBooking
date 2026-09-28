using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;
using CinemaBooking.Domain.Exceptions;
using CinemaBooking.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Domain.Providers.Implementation;

internal class SeatProvider(
    ICinemaBookingContext context,
    IContiguousSeatFinder contiguousSeatFinder) : ISeatProvider
{
    public async Task CreateSeats(long showTimeId, CancellationToken cancellationToken)
    {
        var number = 1;

        var seats = new List<Seat>
        {
            CreateSeat(showTimeId, 1, number++),
            CreateSeat(showTimeId, 1, number++),
            CreateSeat(showTimeId, 1, number++),
            CreateSeat(showTimeId, 1, number++),
            CreateSeat(showTimeId, 1, number++),
            CreateSeat(showTimeId, 1, number++),

            CreateSeat(showTimeId, 2, number++),
            CreateSeat(showTimeId, 2, number++),
            CreateSeat(showTimeId, 2, number++),
            CreateSeat(showTimeId, 2, number++),
            CreateSeat(showTimeId, 2, number++),
            CreateSeat(showTimeId, 2, number++),

            CreateSeat(showTimeId, 3, number++),
            CreateSeat(showTimeId, 3, number++),
            CreateSeat(showTimeId, 3, number++),
            CreateSeat(showTimeId, 3, number++),
            CreateSeat(showTimeId, 3, number++),
            CreateSeat(showTimeId, 3, number++)
        };

        await context.Seats.AddRangeAsync(seats, cancellationToken);

        await context.SaveChanges(cancellationToken);
    }

    public async Task<BookingResponse> BookingSeats(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        var showTime = await GetShowTime(bookingRequest.ShowTimeId, cancellationToken);
        var seats = await GetSeats(bookingRequest, cancellationToken);

        var isAllAvailable = seats.All(seat => seat.Status == (short)SeatStatus.Available);

        if (!isAllAvailable)
        {
            throw new InvalidOperationException("One or more seats are not available for booking.");
        }

        foreach (var seat in seats)
        {
            seat.UpdateStatus(SeatStatus.Reserved, DateTime.UtcNow);
        }

        await context.SaveChanges(cancellationToken);

        var response = CreateBookingResponse(showTime, seats);

        return response;
    }

    public async Task ConfirmBookingSeats(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        var seats = await GetSeats(bookingRequest, cancellationToken);

        var areNoRejectedSeats = seats.All(seat =>
                                      seat.Status == (short)SeatStatus.Reserved &&
                                      DateTime.FromBinary(seat.ReservationTime!.Value).AddMinutes(10) > DateTime.UtcNow);

        var isContaineSoldSeat = seats.Any(s => s.Status == (short)SeatStatus.Sold);

        try
        {
            if (!areNoRejectedSeats)
            {
                if (isContaineSoldSeat)
                {
                    throw new ReservetionException("One or more seats are already sold. Only applications for reserved seats were rejected.");
                }

                throw new InvalidOperationException("One or more seats are not reserved for booking or have expired.");
            }

            foreach (var seat in seats)
            {
                seat.UpdateStatus(SeatStatus.Sold, DateTime.UtcNow);
            }
        }
        catch (ReservetionException)
        {
            foreach (var seat in seats.Where(s => s.Status == (short)SeatStatus.Reserved))
            {
                seat.UpdateStatus(SeatStatus.Available, DateTime.UtcNow);
            }

            throw;
        }
        catch (Exception)
        {
            foreach (var seat in seats)
            {
                seat.UpdateStatus(SeatStatus.Available, DateTime.UtcNow);
            }

            throw;
        }
        finally
        {
            await context.SaveChanges(cancellationToken);
        }
    }

    public async Task<BookingResponse> BookingContiguousSeats(int count, long showTimeId, CancellationToken cancellationToken)
    {
        var showTime = await GetShowTime(showTimeId, cancellationToken);

        var seats = await context.Seats.Where(s => s.ShowTimeId == showTimeId)
            .ToListAsync(cancellationToken);

        if (seats.Count == 0)
        {
            throw new NotFoundException($"No seats found for the given show time. ShowTimeId = {showTimeId}");
        }

        var contiguousSeats = contiguousSeatFinder.FindContiguousSeats(seats, count);

        foreach (var seat in contiguousSeats)
        {
            seat.UpdateStatus(SeatStatus.Reserved, DateTime.UtcNow);
        }

        await context.SaveChanges(cancellationToken);

        var response = CreateBookingResponse(showTime, seats);

        return response;
    }

    private BookingResponse CreateBookingResponse(Showtime showTime, List<Seat> seats)
    {
        var response = new BookingResponse
        {
            ShowtimeInfo = $"Showtime info: Start - {showTime.StartTime} / Auditorium - {showTime.AuditoriumId}",
            MovieInfo = $"Movie info: Title - {showTime.Movie.Title} / Year - {showTime.Movie.Year}",
            SeatsInfo = seats.GroupBy(s => s.Row)
           .Select(seat => $"Seats info: Row - {seat.Key} / Number -> {string.Join(", ", seat.Select(s => s.Number))}")
        };

        return response;
    }

    private async Task<Showtime> GetShowTime(long showTimeId, CancellationToken cancellationToken)
    {
        var showTime = await context.Showtimes.AsNoTracking()
            .Include(dto => dto.Movie)
            .SingleOrDefaultAsync(st => st.Id == showTimeId, cancellationToken);

        if (showTime == null)
        {
            throw new NotFoundException($"Showtime with id {showTimeId} not found.");
        }

        return showTime;
    }

    private async Task<List<Seat>> GetSeats(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        var seats = await context
            .Seats
            .Where(dto => dto.ShowTimeId == bookingRequest.ShowTimeId && bookingRequest.SeatIds.Contains(dto.Id))
            .ToListAsync(cancellationToken);

        if (seats.Count != bookingRequest.SeatIds.Distinct().Count())
        {
            throw new NotFoundException("No valid seats found for booking.");
        }

        return seats;
    }

    private Seat CreateSeat(long showTimeId, int row, int number)
    {
        return new Seat
        {
            ShowTimeId = showTimeId,
            Row = row,
            Number = number,
            Status = (short)SeatStatus.Available,
            Version = Guid.NewGuid()
        };
    }
}