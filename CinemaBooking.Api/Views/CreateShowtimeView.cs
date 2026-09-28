using System.Text.Json.Serialization;

namespace CinemaBooking.Api.Views;

public class CreateShowtimeView
{
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }

    [JsonPropertyName("movieId")]
    public long MovieId { get; set; }

    [JsonPropertyName("auditoriumId")]
    public long AuditoriumId { get; set; }
}