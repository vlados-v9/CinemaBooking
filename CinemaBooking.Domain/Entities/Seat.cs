using CinemaBooking.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Domain.Entities;

public class Seat
{
    public long Id { get; set; }
    public int Row { get; set; }
    public int Number { get; set; }
    public long ShowTimeId { get; set; }
    public short Status { get; set; }
    public long? ReservationTime { get; set; }

    //This feature may not be necessary if we are using SQLite,
    //as the file is locked for editing/updating and cannot be accessed by two users at the same time.
    //However, for other databases that allow multiple users to perform operations on the database at the same time,
    //this will work. (It should work 😁)
    [ConcurrencyCheck]
    public Guid Version { get; set; } = Guid.NewGuid();

    public void UpdateStatus(SeatStatus newStatus, DateTime utcNow)
    {
        Status = (short)newStatus;
        ReservationTime = utcNow.ToBinary();
        Version = Guid.NewGuid();
    }
}
