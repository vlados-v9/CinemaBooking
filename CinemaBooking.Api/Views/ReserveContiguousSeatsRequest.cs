using System.Text.Json.Serialization;

namespace CinemaBooking.Api.Views;

public class ReserveContiguousSeatsRequest
{
    [JsonPropertyName("showtimeId")]
    public long ShowTimeId { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}