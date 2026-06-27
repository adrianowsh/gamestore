using System.Diagnostics;

namespace GameStore.Api.Middlewares;

public class RequestTimingMiddleware(
    RequestDelegate next,
    ILogger<RequestTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();

        try
        {
            stopwatch.Start();
            await next(context);
        }
        finally
        {
            stopwatch.Stop();

            var elapsedTime = stopwatch.ElapsedMilliseconds;

            logger.LogInformation("Request {RequestMethod} {RequestPath} processing time: {ElapsedTime} ms",
                context.Request.Method,
                context.Request.Path,
                elapsedTime);
        }
    }
}

