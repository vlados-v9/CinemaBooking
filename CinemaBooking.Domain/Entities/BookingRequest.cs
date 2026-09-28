namespace CinemaBooking.Domain.Entities;

public record BookingRequest(long ShowTimeId, HashSet<long> SeatIds);