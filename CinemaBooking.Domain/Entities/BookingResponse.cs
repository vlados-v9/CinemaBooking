using System.Text.Json.Serialization;

namespace CinemaBooking.Domain.Entities;

public sealed class BookingResponse
{
    //[JsonPropertyName("bookingId")]
    //public long BookingId { get; set; }

    [JsonPropertyName("movieInfo")]
    public string MovieInfo { get; set; } = string.Empty;

    [JsonPropertyName("showtimeInfo")]
    public string ShowtimeInfo { get; set; } = string.Empty;

    [JsonPropertyName("seatsInfo")]
    public IEnumerable<string> SeatsInfo { get; set; }
}