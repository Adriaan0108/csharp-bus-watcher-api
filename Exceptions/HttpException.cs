namespace csharp_bus_watcher_api.Exceptions;

public class HttpException : Exception
{
    public HttpException(int statusCode, string title = null, string message = null, Exception innerException = null)
        : base(message ?? title, innerException)
    {
        StatusCode = statusCode;
        Title = title ?? GetDefaultTitle(statusCode);
    }

    public int StatusCode { get; }
    public string Title { get; }

    private static string GetDefaultTitle(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            422 => "Unprocessable Entity",
            503 => "Service Unavailable",
            _ => "Internal Server Error"
        };
    }
}