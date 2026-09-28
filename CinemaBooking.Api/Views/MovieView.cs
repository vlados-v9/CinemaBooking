using System.Text.Json.Serialization;

namespace CinemaBooking.Api.Views;

public sealed class MovieView
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public long Category { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }
}