using System.Net;
using System.Text.Json;

namespace OpeningGym.Users.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _logger);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
    {
        HttpStatusCode status;
        string message = ex.Message;

        switch (ex)
        {
            case ArgumentException:
            case InvalidOperationException:
                status = HttpStatusCode.BadRequest;
                break;

            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                break;

            case KeyNotFoundException:
                status = HttpStatusCode.NotFound;
                break;

            default:
                status = HttpStatusCode.InternalServerError;
                message = "Beklenmeyen bir hata oluştu.";
                logger.LogError(ex, "Unhandled exception caught by middleware");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var response = new
        {
            success = false,
            error = new
            {
                message,
                code = status.ToString(),
                timestamp = DateTime.UtcNow
            }
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
