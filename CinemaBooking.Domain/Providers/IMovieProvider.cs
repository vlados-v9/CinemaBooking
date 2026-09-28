using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Domain.Providers
{
    internal interface IMovieProvider
    {
        Task<Movie> GetMovie(long id, CancellationToken cancellationToken);

        Task<List<Movie>> GetAllMovie(CancellationToken cancellationToken);

        Task<long> AddMovie(Movie movie, CancellationToken cancellationToken);

        Task UpdateMovie(Movie movie, CancellationToken cancellationToken);

        Task DeleteMovie(long id, CancellationToken cancellationToken);
    }
}