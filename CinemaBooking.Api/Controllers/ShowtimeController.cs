using CinemaBooking.Api.Views;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Api.Controllers
{
    [ApiController]
    [Route("api/showtime")]
    public class ShowtimeController(IShowtimeManager showtimeManager) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<long>> CreateShowtime([FromBody] CreateShowtimeView showtime, CancellationToken cancellationToken)
        {
            var showTime = new Showtime()
            {
                //Id = showtime.Id,
                StartTime = showtime.StartTime,
                MovieId = showtime.MovieId,
                AuditoriumId = showtime.AuditoriumId
            };

            var createdShowtimeId = await showtimeManager.Create(showTime, cancellationToken);
            return Ok(createdShowtimeId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShowtimeView>> GetShowtime(long id, CancellationToken cancellationToken)
        {
            var showtime = await showtimeManager.GetById(id, cancellationToken);
            var view = new ShowtimeView()
            {
                Id = showtime.Id,
                StartTime = showtime.StartTime,
                MovieId = showtime.MovieId,
                AuditoriumId = showtime.AuditoriumId,
                Seats = showtime.Seats,
                Movie = new MovieView()
                {
                    Id = showtime.Movie.Id,
                    Title = showtime.Movie.Title,
                    Category = showtime.Movie.Category,
                    Year = showtime.Movie.Year
                }
            };
            return Ok(showtime);
        }
    }
}
