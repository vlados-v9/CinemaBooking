using CinemaBooking.Domain.Entities;
using System.Text.Json.Serialization;

namespace CinemaBooking.Api.Views;

public class ShowtimeView
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }

    [JsonPropertyName("movieId")]
    public long MovieId { get; set; }

    [JsonPropertyName("movie")]
    public MovieView Movie { get; set; }

    [JsonPropertyName("auditoriumId")]
    public long AuditoriumId { get; set; }

    [JsonPropertyName("seats")]
    public List<Seat> Seats { get; set; } = [];
}