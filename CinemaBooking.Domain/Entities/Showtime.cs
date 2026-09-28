namespace CinemaBooking.Domain.Entities;

public class Showtime
{
    public long Id { get; set; }
    public long StartTime { get; set; }

    public long MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public long AuditoriumId { get; set; }

    public List<Seat> Seats { get; set; } = [];
}
