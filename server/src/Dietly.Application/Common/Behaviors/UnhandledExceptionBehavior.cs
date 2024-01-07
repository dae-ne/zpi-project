using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Dietly.Application.Common.Behaviors;

// TODO: add unit tests
public sealed class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
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

            logger.LogError(ex, "Request: Unhandled Exception for Request {RequestName} {@Request}", requestName, request);

            var unknownErrorResultMethod = typeof(TResponse).GetMethod("UnknownError", BindingFlags.Public | BindingFlags.Static);
            var result = unknownErrorResultMethod?.Invoke(null, null);
            return (TResponse)result!; // Tricky, but we have tests for this
        }
    }
}
