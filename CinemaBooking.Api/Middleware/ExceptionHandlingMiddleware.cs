using CinemaBooking.Domain.Exceptions;
using System.Net;

namespace CinemaBooking.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception");
            await WriteProblemAsync(context, exception);
        }
    }

    private static async Task WriteProblemAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            ArgumentException => (HttpStatusCode.BadRequest, "Invalid request"),

            KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
            NotFoundException => (HttpStatusCode.NotFound, "Resource not found"),

            InvalidOperationException => (HttpStatusCode.Conflict, "Operation cannot be completed"),
            ReservetionException => (HttpStatusCode.Conflict, "Operation cannot be completed"),

            _ => (HttpStatusCode.InternalServerError, "Unexpected error")
        };

        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            title,
            detail = exception.Message,
            status = context.Response.StatusCode
        });
    }
}