using CinemaBooking.Domain.Providers;

namespace CinemaBooking.Domain.Manager.Implementation;

/// <summary>
/// Here we can implement authentication to check if actor can perfrom these actions. 
/// For now, we are just passing the request to the provider.
/// </summary>
/// <param name="seatProvider"></param>
internal sealed class SeatManager(ISeatProvider seatProvider) : ISeatManager
{
    public Task CreateSeats(long showTimeId, int countRow, int countSeatsPerRow, CancellationToken cancellationToken) =>
        seatProvider.CreateSeats(showTimeId, countRow, countSeatsPerRow, cancellationToken);

    public Task DeleteSeat(long seatId, CancellationToken cancellationToken) =>
        seatProvider.DeleteSeat(seatId, cancellationToken);
}
