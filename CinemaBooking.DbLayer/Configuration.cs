using CinemaBooking.DbLayer.Context;
using CinemaBooking.Domain.Context;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBooking.DbLayer;

public static class Configuration
{
    public static IServiceCollection AddCinemaBookingDbLayer(this IServiceCollection serviceColelction)
    {
        serviceColelction.AddDbContext<CinemaBookingContext>();
        serviceColelction.AddScoped<ICinemaBookingContext, CinemaBookingContext>();
        return serviceColelction;
    }
}