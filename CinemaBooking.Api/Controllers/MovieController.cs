using CinemaBooking.Api.Views;
using CinemaBooking.Domain.Entities;
using CinemaBooking.Domain.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Api.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController(IMovieManager movieManager) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<ActionResult<List<MovieView>>> GetMovies()
        {
            var movies = await movieManager.GetAllMovie(CancellationToken.None);
            var views = movies.Select(m => new MovieView
            {
                Id = m.Id,
                Title = m.Title,
                Category = m.Category,
                Year = m.Year
            }).ToList();

            return Ok(views);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieView>> GetMovie(long id)
        {
            var movie = await movieManager.GetMovie(id, CancellationToken.None);
            var view = new MovieView
            {
                Id = movie.Id,
                Title = movie.Title,
                Category = movie.Category,
                Year = movie.Year
            };

            return Ok(view);
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> AddMovie([FromBody] MovieView movieView)
        {
            var movieEntity = new Movie
            {
                Id = movieView.Id,
                Title = movieView.Title,
                Category = movieView.Category,
                Year = movieView.Year
            };

            var createdMovieId = await movieManager.AddMovie(movieEntity, CancellationToken.None);

            return Ok(createdMovieId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieView movieView)
        {
            var movieEntity = new Movie
            {
                Id = movieView.Id,
                Title = movieView.Title,
                Category = movieView.Category,
                Year = movieView.Year
            };

            await movieManager.UpdateMovie(movieEntity, CancellationToken.None);

            return NoContent();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveMovie(long id)
        {
            await movieManager.DeleteMovie(id, CancellationToken.None);

            return NoContent();
        }
    }
}
