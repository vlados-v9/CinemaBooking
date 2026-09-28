using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Domain.Providers.Implementation;

internal sealed class MovieProvider(ICinemaBookingContext context, IValidator<Movie> validator) : IMovieProvider
{
    public async Task<long> AddMovie(Movie movie, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(movie, cancellationToken);

        await context.Movies.AddAsync(movie, cancellationToken);
        await context.SaveChanges(cancellationToken);

        return movie.Id;
    }

    public async Task DeleteMovie(long id, CancellationToken cancellationToken)
    {
        var deletedRaws = await context.Movies.Where(m => m.Id == id).ExecuteDeleteAsync();

        if (deletedRaws == 0)
        {
            throw new NotFoundException($"Movie with id {id} not found.");
        }
    }

    public async Task<List<Movie>> GetAllMovie(CancellationToken cancellationToken) =>
        await context.Movies.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Movie> GetMovie(long id, CancellationToken cancellationToken)
    {
        var expectedMovie = await context.Movies.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (expectedMovie == null)
        {
            throw new NotFoundException($"Movie with id {id} not found.");
        }

        return expectedMovie;
    }

    public async Task UpdateMovie(Movie movie, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(movie, cancellationToken);

        var toUpdatedMovie = await context.Movies.SingleOrDefaultAsync(m => m.Id == movie.Id, cancellationToken);

        if (toUpdatedMovie == null)
        {
            throw new NotFoundException($"Movie with id {movie.Id} not found.");
        }

        toUpdatedMovie.Update(movie);

        await context.SaveChanges(cancellationToken);
    }
}