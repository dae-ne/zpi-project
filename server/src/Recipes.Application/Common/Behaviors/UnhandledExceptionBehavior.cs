using Microsoft.Extensions.Logging;

namespace Recipes.Application.Common.Behaviors;

public sealed class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogError(ex, "Request: Unhandled Exception for Request {RequestName} {@Request}",
                requestName, request);

            throw;
        }
    }
}
