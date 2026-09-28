using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;
using CinemaBooking.Domain.Exceptions;
using CinemaBooking.Domain.Interfaces.Services;

namespace CinemaBooking.Domain.Services;

public class ContiguousSeatFinder : IContiguousSeatFinder
{
    public List<Seat> FindContiguousSeats(IEnumerable<Seat> seats, int count)
    {
        if (count <= 0)
        {
            throw new InvalidOperationException("Invalid input parameters: count must be positive.");
        }

        var seatsByRow = seats.GroupBy(s => s.Row);

        foreach (var rowGroup in seatsByRow)
        {
            var orderedSeats = rowGroup.OrderBy(s => s.Number).ToList();

            var contiguousBlock = new List<Seat>();

            foreach (var seat in orderedSeats)
            {
                if (seat.Status != (short)SeatStatus.Available)
                {
                    contiguousBlock.Clear();
                    continue;
                }

                contiguousBlock.Add(seat);

                if (contiguousBlock.Count == count)
                {
                    return contiguousBlock;
                }
            }
        }

        throw new NotFoundException("No contiguous seats found.");
    }
}
