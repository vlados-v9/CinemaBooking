using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Domain.Manager;

public interface ISeatManager
{
    Task CreateSeats(long showTimeId, CancellationToken cancellationToken);

    Task<BookingResponse> BookingSeats(BookingRequest bookingRequest, CancellationToken cancellationToken);

    Task<BookingResponse> BookingContiguousSeats(int count, long showTimeId, CancellationToken cancellationToken);

    Task ConfirmBookingSeats(BookingRequest bookingRequest, CancellationToken cancellationToken);
}
