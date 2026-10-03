using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Providers;

namespace CinemaBooking.Domain.Manager.Implementation;

public sealed class BookingManager(IBookingProvider bookingProvider) : IBookingManager
{
    public Task ConfirmBooking(Guid bookingId, CancellationToken cancellationToken) =>
        bookingProvider.ConfirmBooking(bookingId, cancellationToken);

    public Task<BookingResponse> CreateBooking(BookingRequest bookingRequest, CancellationToken cancellationToken) =>
        bookingProvider.CreateBooking(bookingRequest, cancellationToken);

    public Task<BookingResponse> CreateBookingForContiguous(int count, long showTimeId, CancellationToken cancellationToken) =>
        bookingProvider.CreateBookingForContiguous(count, showTimeId, cancellationToken);

    public Task DeleteBooking(Guid bookingId, CancellationToken cancellationToken) =>
        bookingProvider.DeleteBooking(bookingId, cancellationToken);

    public Task<List<Booking>> GetAllBooking(CancellationToken cancellationToken) =>
        bookingProvider.GetAllBooking(cancellationToken);

    public Task UpdateBooking(BookingUpdateRequest bookingRequest, CancellationToken cancellationToken) =>
        bookingProvider.UpdateBooking(bookingRequest, cancellationToken);
}