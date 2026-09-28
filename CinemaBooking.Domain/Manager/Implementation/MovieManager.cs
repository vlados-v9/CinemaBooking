using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Providers;

namespace CinemaBooking.Domain.Manager.Implementation;

/// <summary>
/// Here we can implement authentication to check if actor can perfrom these actions. 
/// For now, we are just passing the request to the provider.
/// </summary>
/// <param name="movieProvider"></param>
internal sealed class MovieManager(IMovieProvider movieProvider) : IMovieManager
{
    public Task<long> AddMovie(Movie movie, CancellationToken cancellationToken) =>
        movieProvider.AddMovie(movie, cancellationToken);

    public Task DeleteMovie(long id, CancellationToken cancellationToken) =>
        movieProvider.DeleteMovie(id, cancellationToken);

    public Task<List<Movie>> GetAllMovie(CancellationToken cancellationToken) =>
        movieProvider.GetAllMovie(cancellationToken);

    public Task<Movie> GetMovie(long id, CancellationToken cancellationToken) =>
        movieProvider.GetMovie(id, cancellationToken);

    public Task UpdateMovie(Movie movie, CancellationToken cancellationToken) =>
        movieProvider.UpdateMovie(movie, cancellationToken);
}