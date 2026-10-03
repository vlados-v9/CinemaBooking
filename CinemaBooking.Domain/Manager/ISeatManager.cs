namespace CinemaBooking.Domain.Manager;

public interface ISeatManager
{
    Task CreateSeats(long showTimeId, int countRow, int countSeatsPerRow, CancellationToken cancellationToken);
    Task DeleteSeat(long seatId, CancellationToken cancellationToken);
}
