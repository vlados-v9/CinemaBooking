using CinemaBooking.Api.Views;
using CinemaBooking.Domain.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Api.Controllers;

[ApiController]
[Route("api/seat")]
public class SeatController(ISeatManager seatManager) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateSeats([FromBody] CreateSeatsRequest request, CancellationToken cancellationToken)
    {
        await seatManager.CreateSeats(request.ShowTimeId, request.CountRow, request.CountSeatsPerRow, cancellationToken);
        return NoContent();
    }

    [HttpDelete("delete/{seatId}")]
    public async Task<IActionResult> DeleteSeat(long seatId, CancellationToken cancellationToken)
    {
        await seatManager.DeleteSeat(seatId, cancellationToken);
        return NoContent();
    }
}
