namespace CinemaBooking.Domain.Providers
{
    /// <summary>
    /// Accoring to task we don't need to manage auditorium and as the result also we don't need to manage seats.
    /// So this interface is created to provide a method for creating seats for ShowTime when it is created. 
    /// This is a temporary solution and in real world scenario we should have a better solution for managing seats.
    /// </summary>
    internal interface ISeatProvider
    {
        Task CreateSeats(long showTimeId, int countRow, int countSeatsPerRow, CancellationToken cancellationToken);
        Task DeleteSeat(long seatId, CancellationToken cancellationToken);
    }
}
