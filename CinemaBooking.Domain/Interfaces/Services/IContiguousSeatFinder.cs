using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Domain.Interfaces.Services;

public interface IContiguousSeatFinder
{
    List<Seat> FindContiguousSeats(IEnumerable<Seat> seats, int count);
}
