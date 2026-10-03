namespace CinemaBooking.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public long ShowTimeId { get; set; }
    public List<long> ReservedSeatIds { get; set; } = new List<long>();
    public DateTimeOffset BookingTime { get; set; }

    public void Update(Booking booking)
    {
        ShowTimeId = booking.ShowTimeId;
        ReservedSeatIds = booking.ReservedSeatIds;
        BookingTime = booking.BookingTime;
    }
}