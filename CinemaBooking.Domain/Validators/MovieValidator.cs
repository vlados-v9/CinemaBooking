using CinemaBooking.Domain.Entities;
using FluentValidation;

namespace CinemaBooking.Domain.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(movie => movie.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");
        RuleFor(movie => movie.Category)
            .GreaterThan(0).WithMessage("Category must be a positive number.");
        RuleFor(movie => movie.Year)
            .InclusiveBetween(1900, DateTime.Now.Year + 3).WithMessage($"Year must be between 1900 and {DateTime.Now.Year + 3}.");
    }
}
