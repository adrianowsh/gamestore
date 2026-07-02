using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace GameStore.Api.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.TraceId;

        logger.LogError(
            exception,
            "Could not process on machine {Machine} - TraceId: {TraceId}",
            Environment.MachineName,
            traceId);

        await Results.Problem(
            title: "An error occured while processing your request.",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?>()
            {
                { "traceId", traceId.ToString() }
            }
        ).ExecuteAsync(httpContext);

        return true;
    }
}