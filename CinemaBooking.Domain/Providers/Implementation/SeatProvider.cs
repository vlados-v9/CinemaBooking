using CinemaBooking.Domain.Context;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Enums;
using CinemaBooking.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Domain.Providers.Implementation;

internal class SeatProvider(ICinemaBookingContext context) : ISeatProvider
{
    public async Task CreateSeats(long showTimeId, int countRow, int countSeatsPerRow, CancellationToken cancellationToken)
    {
        var seats = GenerateSeats(showTimeId, countRow, countSeatsPerRow);

        await context.Seats.AddRangeAsync(seats, cancellationToken);

        await context.SaveChanges(cancellationToken);
    }

    public async Task DeleteSeat(long seatId, CancellationToken cancellationToken)
    {
        var seat = await context.Seats.SingleOrDefaultAsync(s => s.Id == seatId, cancellationToken)
            ?? throw new NotFoundException($"Seat with id {seatId} not found");

        context.Seats.Remove(seat);

        await context.SaveChanges(cancellationToken);
    }

    private IEnumerable<Seat> GenerateSeats(long showTimeId, int countRow, int countSeatsPerRow)
    {
        for (int row = 1; row <= countRow; row++)
        {
            for (int number = 1; number <= countSeatsPerRow; number++)
            {
                yield return new Seat
                {
                    ShowTimeId = showTimeId,
                    Row = row,
                    Number = number,
                    Status = (short)SeatStatus.Available,
                    Version = Guid.NewGuid()
                };
            }
        }
    }
}