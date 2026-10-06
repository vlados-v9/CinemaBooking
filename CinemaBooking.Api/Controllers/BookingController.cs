using CinemaBooking.Api.Views;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Api.Controllers;

[ApiController]
[Route("api/booking")]
public class BookingController(IBookingManager bookingManager) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<List<Booking>>> GetAllBookings()
    {
        var bookings = await bookingManager.GetAllBooking(CancellationToken.None);
        return Ok(bookings);
    }

    [HttpPost("create")]
    public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] BookingRequest bookingRequest)
    {
        var bookingId = await bookingManager.CreateBooking(bookingRequest, CancellationToken.None);
        return Ok(bookingId);
    }

    [HttpPost("createContiguous")]
    public async Task<ActionResult<BookingResponse>> CreateContiguousBooking([FromBody] ReserveContiguousSeatsRequest bookingRequest)
    {
        var bookingId = await bookingManager.CreateBookingForContiguous(bookingRequest.Count, bookingRequest.ShowTimeId, CancellationToken.None);
        return Ok(bookingId);
    }

    [HttpDelete("delete/{bookingId}")]
    public async Task<IActionResult> DeleteBooking(Guid bookingId)
    {
        await bookingManager.DeleteBooking(bookingId, CancellationToken.None);
        return NoContent();
    }

    [HttpPut("confirmBooking/{bookingId}")]
    public async Task<IActionResult> ConfirmBooking(Guid bookingId)
    {
        await bookingManager.ConfirmBooking(bookingId, CancellationToken.None);
        return NoContent();
    }

    [HttpPut("updateBooking")]
    public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateRequest bookingUpdateRequest)
    {
        await bookingManager.UpdateBooking(bookingUpdateRequest, CancellationToken.None);
        return NoContent();
    }
}