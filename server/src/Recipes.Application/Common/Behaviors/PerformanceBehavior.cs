using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Recipes.Application.Common.Behaviors;

public sealed class PerformanceBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();

        timer.Start();

        var response = await next();

        timer.Stop();

        var elapsedMilliseconds = timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 500)
        {
            return response;
        }

        var requestName = typeof(TRequest).Name;

        logger.LogWarning(
            "Long Running Request: {RequestName} ({ElapsedMilliseconds} ms) {@Request}",
            requestName, elapsedMilliseconds, request);

        return response;
    }
}
