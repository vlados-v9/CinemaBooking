using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Domain.Providers
{
    internal interface IShowtimeProvider
    {
        Task<long> Create(Showtime showTime, CancellationToken cancellationToken);
        Task<Showtime> GetById(long id, CancellationToken cancellationToken);
    }
}
