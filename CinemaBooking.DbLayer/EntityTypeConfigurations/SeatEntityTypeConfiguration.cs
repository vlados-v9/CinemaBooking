using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBooking.DbLayer.EntityTypeConfigurations;

internal class SeatEntityTypeConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("Seat");

        builder.HasKey(s => s.Id);

        builder.Property(dto => dto.Row).HasColumnName("Row");

        builder.Property(dto => dto.Number).HasColumnName("Number");

        builder.Property(dto => dto.ShowTimeId).HasColumnName("ShowTimeId");

        builder.Property(dto => dto.Status).HasColumnName("Status");

        builder.Property(dto => dto.ReservationTime).HasColumnName("ReservationTime").IsRequired(false);

        builder.Property(dto => dto.Version).HasColumnName("Version");
    }
}