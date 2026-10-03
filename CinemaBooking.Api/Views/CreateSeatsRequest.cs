namespace CinemaBooking.Api.Views;

public record CreateSeatsRequest(long ShowTimeId, int CountRow, int CountSeatsPerRow);
