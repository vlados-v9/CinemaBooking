namespace CinemaBooking.Domain.Entities;

public class Movie
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public long Category { get; set; }
    public int Year { get; set; }

    public void Update(Movie movie)
    {
        Title = movie.Title;
        Category = movie.Category;
        Year = movie.Year;
    }
}
