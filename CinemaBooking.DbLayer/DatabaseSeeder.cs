using Bogus;
using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.DbLayer;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
       ICinemaBookingContext context,
       CancellationToken cancellationToken = default)
    {
        //await context.Database.MigrateAsync(cancellationToken);

        if (!await context.Movies.AnyAsync(cancellationToken))
        {
            var movies = GenerateMovies(10);

            await context.Movies.AddRangeAsync(
                movies,
                cancellationToken);

            await context.SaveChanges(cancellationToken);
        }

        if (!await context.Showtimes.AnyAsync(cancellationToken))
        {
            var movies = await context.Movies.AsNoTracking().ToListAsync(cancellationToken);

            var showtimes = GenerateShowtimes(movies.Select(m => m.Id), count: 3);

            await context.Showtimes.AddRangeAsync(
                showtimes,
                cancellationToken);

            await context.SaveChanges(cancellationToken);
        }

        if (!await context.Seats.AnyAsync(cancellationToken))
        {
            var showtimes = await context.Showtimes.AsNoTracking().ToListAsync(cancellationToken);

            var seats = GenerateSeats(showtimes.Select(showtime => showtime.Id));

            await context.Seats.AddRangeAsync(
                seats,
                cancellationToken);

            await context.SaveChanges(cancellationToken);
        }
    }

    private static List<Movie> GenerateMovies(int count)
    {
        var faker = new Faker<Movie>()
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Category, f => f.Random.Long(1, 5))
            .RuleFor(x => x.Year, f => f.Random.Int(1950, 2026));

        return faker.Generate(count);
    }

    private static List<Showtime> GenerateShowtimes(
        IEnumerable<long> movieIds,
        int count)
    {
        var faker = new Faker<Showtime>()
            .RuleFor(x => x.StartTime, f =>
                DateTimeOffset.UtcNow
                    .AddDays(f.Random.Int(0, 7))
                    .AddHours(f.Random.Int(10, 22)))
            .RuleFor(x => x.MovieId, f => f.PickRandom(movieIds))
            .RuleFor(x => x.AuditoriumId, f => f.Random.Long(1, 5));

        return faker.Generate(count);
    }

    private static List<Seat> GenerateSeats(IEnumerable<long> showtimeIds)
    {
        var seats = new List<Seat>();

        foreach (var showtimeId in showtimeIds)
        {
            for (var row = 1; row <= 3; row++)
            {
                for (var number = 1; number <= 10; number++)
                {
                    seats.Add(new Seat
                    {
                        Row = row,
                        Number = number,
                        ShowTimeId = showtimeId,
                        Status = (short)SeatStatus.Available,
                        ReservationTime = null,
                        Version = Guid.NewGuid()
                    });
                }
            }
        }

        return seats;
    }
}