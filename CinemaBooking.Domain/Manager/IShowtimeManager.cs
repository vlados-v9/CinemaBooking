using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Domain.Manager
{
    public interface IShowtimeManager
    {
        Task<long> Create(Showtime showTime, CancellationToken cancellationToken);
        Task<Showtime> GetById(long id, CancellationToken cancellationToken);
    }
}
