using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Domain.Providers.Implementation;

/// <summary>
/// As we do not implement auditorium management, we will create seats for the showtime when it is created. 
/// This is a temporary solution and in a real-world scenario, we should have a better solution for managing seats.
/// </summary>
/// <param name="seatProvider"></param>
internal class ShowtimeProvider(
    ICinemaBookingContext context,
    IValidator<Showtime> validator) : IShowtimeProvider
{
    public async Task<long> Create(Showtime showTime, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(showTime, cancellationToken);

        var movies = await context.Movies.Where(m => m.Id == showTime.MovieId).ToListAsync(cancellationToken);

        if (movies.Count == 0)
        {
            throw new NotFoundException($"Movie with id {showTime.MovieId} not found.");
        }

        await context.Showtimes.AddAsync(showTime, cancellationToken);

        await context.SaveChanges(cancellationToken);

        return showTime.Id;
    }

    public async Task<Showtime> GetById(long id, CancellationToken cancellationToken)
    {
        var showTime = await context
            .Showtimes
            .AsNoTracking()
            .Include(dto => dto.Movie)
            .Include(dto => dto.Seats)
            .SingleOrDefaultAsync(dto => dto.Id == id, cancellationToken);

        if (showTime == null)
        {
            throw new NotFoundException($"Showtime with id {id} not found");
        }

        return showTime;
    }
}