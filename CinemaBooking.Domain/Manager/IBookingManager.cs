using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Domain.Manager;

public interface IBookingManager
{
    Task<List<Booking>> GetAllBooking(CancellationToken cancellationToken);
    Task<BookingResponse> CreateBooking(BookingRequest bookingRequest, CancellationToken cancellationToken);
    Task<BookingResponse> CreateBookingForContiguous(int count, long showTimeId, CancellationToken cancellationToken);
    Task UpdateBooking(BookingUpdateRequest bookingRequest, CancellationToken cancellationToken);
    Task ConfirmBooking(Guid bookingId, CancellationToken cancellationToken);
    Task DeleteBooking(Guid bookingId, CancellationToken cancellationToken);
}
