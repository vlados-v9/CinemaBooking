using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Providers;

namespace CinemaBooking.Domain.Manager.Implementation;

/// <summary>
/// Here we can implement authentication to check if actor can perfrom these actions. 
/// For now, we are just passing the request to the provider.
/// </summary>
/// <param name="seatProvider"></param>
internal sealed class SeatManager(ISeatProvider seatProvider) : ISeatManager
{
    public Task<BookingResponse> BookingSeats(BookingRequest bookingRequest, CancellationToken cancellationToken) =>
        seatProvider.BookingSeats(bookingRequest, cancellationToken);

    public Task<BookingResponse> BookingContiguousSeats(int count, long showTimeId, CancellationToken cancellationToken) =>
        seatProvider.BookingContiguousSeats(count, showTimeId, cancellationToken);

    public Task ConfirmBookingSeats(BookingRequest bookingRequest, CancellationToken cancellationToken) =>
        seatProvider.ConfirmBookingSeats(bookingRequest, cancellationToken);

    public Task CreateSeats(long showTimeId, CancellationToken cancellationToken) =>
        seatProvider.CreateSeats(showTimeId, cancellationToken);
}
