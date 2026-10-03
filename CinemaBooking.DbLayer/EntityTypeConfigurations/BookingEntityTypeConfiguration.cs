using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaBooking.DbLayer.EntityTypeConfigurations;

internal class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(dto => dto.Id);

        builder.Property(dto => dto.ShowTimeId).HasColumnName("ShowTimeId").IsRequired();

        builder.HasOne<Showtime>()
            .WithMany()
            .HasForeignKey(x => x.ShowTimeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(dto => dto.ReservedSeatIds).HasColumnName("ReservedSeatIds").IsRequired()
            .HasConversion(
            seatsId => JsonSerializer.Serialize(seatsId),
            value => JsonSerializer.Deserialize<List<long>>(value)!);

        builder.Property(dto => dto.BookingTime).HasColumnName("BookingTime").IsRequired();
    }
}