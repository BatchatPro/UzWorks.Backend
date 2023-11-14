using UzWorks.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace UzWorks.Infrastructure.ExceptionHandling;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UzWorksUnauthorizedException)
        {
            await HandleUnauthorizedException(context);
        }
        catch(UzWorksException ex)
        {
            await HandleUzWorksException(ex, context);
        }
        catch(Exception ex)
        {
            await HandleException(ex, context);
        }
    }

    private async Task HandleUzWorksException(UzWorksException ex, HttpContext context)
    {
        _logger.LogError($"{ex.Message} {Environment.NewLine}Trace: {ex.StackTrace}");

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json"; 
        await context.Response.WriteAsync(JsonSerializer.Serialize(new {error = ex.Message}));
    }

    private async Task HandleException(Exception ex, HttpContext context)
    {
        _logger.LogError($"{ex.Message} {Environment.NewLine}Trace: {ex.StackTrace}");

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(new { error = "Something went wrong. Please, contact administrator" }));
    }

    private static async Task HandleUnauthorizedException(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        await context.Response.WriteAsync("Your token has expired. Please login to the system");
    }
}
