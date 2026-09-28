using CinemaBooking.Api.Views;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Api.Controllers;

[ApiController]
[Route("api/booking")]
public class BookingController(ISeatManager seatManager) : ControllerBase
{
    [HttpPut("reserve")]
    public async Task<ActionResult<BookingResponse>> ReserveSeats([FromBody] BookingRequest bookingRequest)
    {
        var response = await seatManager.BookingSeats(bookingRequest, CancellationToken.None);

        return Ok(response);
    }

    [HttpPut("reserveContiguous")]
    public async Task<ActionResult<BookingResponse>> ReserveContiguousSeats([FromBody] ReserveContiguousSeatsRequest bookingRequest)
    {
        var response = await seatManager.BookingContiguousSeats(bookingRequest.Count, bookingRequest.ShowTimeId, CancellationToken.None);

        return Ok(response);
    }

    [HttpPut("confirm")]
    public async Task<IActionResult> ConfirmSeats([FromBody] BookingRequest bookingRequest)
    {
        await seatManager.ConfirmBookingSeats(bookingRequest, CancellationToken.None);

        return NoContent();
    }
}
