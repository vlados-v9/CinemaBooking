using CinemaBooking.Domain.Entities;
using FluentValidation;

namespace CinemaBooking.Domain.Validators;

public class ShowtimeValidator : AbstractValidator<Showtime>
{
    public ShowtimeValidator()
    {
        //RuleFor(s => DateTime.FromBinary(s.StartTime))
        //    .GreaterThan(DateTime.UtcNow)
        //    .WithMessage("Start time must be in the future.");
        RuleFor(s => s.MovieId)
            .GreaterThan(0)
            .WithMessage("Movie ID must be greater than 0.");
        RuleFor(s => s.AuditoriumId)
            .GreaterThan(0)
            .WithMessage("Auditorium ID must be greater than 0.");
    }
}
