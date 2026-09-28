using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Providers;

namespace CinemaBooking.Domain.Manager.Implementation;

/// <summary>
/// Here we can implement authentication to check if actor can perfrom these actions. 
/// For now, we are just passing the request to the provider.
/// </summary>
internal sealed class ShowtimeManager(IShowtimeProvider showtimeProvider) : IShowtimeManager
{
    public Task<long> Create(Showtime showTime, CancellationToken cancellationToken) =>
        showtimeProvider.Create(showTime, cancellationToken);

    public Task<Showtime> GetById(long id, CancellationToken cancellationToken) =>
        showtimeProvider.GetById(id, cancellationToken);
}
