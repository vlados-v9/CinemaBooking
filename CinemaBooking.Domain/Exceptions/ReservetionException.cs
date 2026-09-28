namespace CinemaBooking.Domain.Exceptions;

public sealed class ReservetionException(string message) : Exception(message)
{
}
