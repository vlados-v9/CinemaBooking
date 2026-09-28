using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Domain.Context;

public interface ICinemaBookingContext
{
    public DbSet<Movie> Movies { get; }

    public DbSet<Seat> Seats { get; }

    public DbSet<Showtime> Showtimes { get; }

    Task SaveChanges(CancellationToken cancellationToken);
}