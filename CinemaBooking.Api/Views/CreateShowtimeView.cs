using System.Text.Json.Serialization;

namespace CinemaBooking.Api.Views;

public class CreateShowtimeView
{
    [JsonPropertyName("startTime")]
    public DateTimeOffset StartTime { get; set; }

    [JsonPropertyName("movieId")]
    public long MovieId { get; set; }

    [JsonPropertyName("auditoriumId")]
    public long AuditoriumId { get; set; }
}