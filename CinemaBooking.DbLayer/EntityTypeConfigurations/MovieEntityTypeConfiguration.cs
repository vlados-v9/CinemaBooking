using CinemaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBooking.DbLayer.EntityTypeConfigurations;

internal class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movie");

        builder.HasKey(dto => dto.Id);

        builder.Property(dto => dto.Title).HasColumnName("Title").IsRequired().HasMaxLength(100);
        builder.Property(dto => dto.Category).HasColumnName("Category").IsRequired();
        builder.Property(dto => dto.Year).HasColumnName("Year").IsRequired();
    }
}