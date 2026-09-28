using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Interfaces.Services;
using CinemaBooking.Domain.Manager;
using CinemaBooking.Domain.Manager.Implementation;
using CinemaBooking.Domain.Providers;
using CinemaBooking.Domain.Providers.Implementation;
using CinemaBooking.Domain.Services;
using CinemaBooking.Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBooking.Domain;

public static class Configuration
{
    public static IServiceCollection AddCinemaBooking(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMovieProvider, MovieProvider>();
        serviceCollection.AddScoped<IMovieManager, MovieManager>();
        serviceCollection.AddScoped<ISeatProvider, SeatProvider>();
        serviceCollection.AddScoped<IShowtimeProvider, ShowtimeProvider>();
        serviceCollection.AddScoped<IShowtimeManager, ShowtimeManager>();
        serviceCollection.AddScoped<ISeatManager, SeatManager>();

        serviceCollection.AddSingleton<IContiguousSeatFinder, ContiguousSeatFinder>();

        serviceCollection.AddSingleton<IValidator<Movie>, MovieValidator>();
        serviceCollection.AddSingleton<IValidator<Showtime>, ShowtimeValidator>();

        return serviceCollection;
    }
}