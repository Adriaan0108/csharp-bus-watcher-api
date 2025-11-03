namespace csharp_bus_watcher_api.Exceptions;

public static class HttpExceptionFactory
{
    public static HttpException BadRequest(string message = null, Exception inner = null)
    {
        return new HttpException(400, "Bad Request", message, inner);
    }

    public static HttpException Unauthorized(string message = null, Exception inner = null)
    {
        return new HttpException(401, "Unauthorized", message, inner);
    }

    public static HttpException Forbidden(string message = null, Exception inner = null)
    {
        return new HttpException(403, "Forbidden", message, inner);
    }

    public static HttpException NotFound(string message = null, Exception inner = null)
    {
        return new HttpException(404, "Not Found", message, inner);
    }

    public static HttpException Conflict(string message = null, Exception inner = null)
    {
        return new HttpException(409, "Conflict", message, inner);
    }

    public static HttpException UnprocessableEntity(string message = null, Exception inner = null)
    {
        return new HttpException(422, "Unprocessable Entity", message, inner);
    }

    public static HttpException ServiceUnavailable(string message = null, Exception inner = null)
    {
        return new HttpException(503, "Service Unavailable", message, inner);
    }
}