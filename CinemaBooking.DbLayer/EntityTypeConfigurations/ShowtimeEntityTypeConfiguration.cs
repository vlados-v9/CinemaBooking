using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBooking.DbLayer.EntityTypeConfigurations;

internal class ShowtimeEntityTypeConfiguration : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        builder.ToTable("Showtime");

        builder.HasKey(dto => dto.Id);

        builder.Property(dto => dto.StartTime).HasColumnName("StartTime");
        builder.Property(dto => dto.MovieId).HasColumnName("MovieId");
        builder.Property(dto => dto.AuditoriumId).HasColumnName("AuditoriumId");

        builder.HasOne(t => t.Movie)
            .WithMany()
            .HasForeignKey(t => t.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Seats)
            .WithOne()
            .HasForeignKey(s => s.ShowTimeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}