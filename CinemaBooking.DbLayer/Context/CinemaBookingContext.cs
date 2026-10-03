using CinemaBooking.Domain;
using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.DbLayer.Context;

internal sealed class CinemaBookingContext : DbContext, ICinemaBookingContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    public DbSet<Showtime> Showtimes => Set<Showtime>();

    public DbSet<Seat> Seats => Set<Seat>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public Task SaveChanges(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionStringToDb.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaBookingContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}