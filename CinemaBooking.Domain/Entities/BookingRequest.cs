namespace CinemaBooking.Domain.Entities;

public record BookingRequest(long ShowTimeId, HashSet<long> SeatIds);
public record BookingUpdateRequest(Guid Id, long ShowTimeId, HashSet<long> SeatIds);