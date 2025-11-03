using System.Text.Json;
using csharp_bus_watcher_api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace csharp_bus_watcher_api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var (statusCode, title) = error is HttpException httpEx
                ? (httpEx.StatusCode, httpEx.Title)
                : (500, "Internal Server Error");

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Type = $"https://httpstatuses.com/{statusCode}",
                Title = title,
                Status = statusCode,
                Detail = error.Message ?? "An error occurred",
                Instance = context.Request.Path
            };

            var isDevelopment = context.RequestServices.GetService<IHostEnvironment>()?.IsDevelopment() ?? false;

            // Add inner exception as separate property only in development
            if (error.InnerException != null && isDevelopment)
                problemDetails.Extensions["innerException"] = new
                {
                    message = error.InnerException.Message,
                    type = error.InnerException.GetType().Name,
                    stackTrace = error.InnerException.StackTrace
                };

            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow.ToString("O");

            await context.Response.WriteAsJsonAsync(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}