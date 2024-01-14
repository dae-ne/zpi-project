using System.Reflection;
using Dietly.Application.Common.Results;
using Microsoft.Extensions.Logging;
using static Dietly.Application.Common.Results.Errors;

namespace Dietly.Application.Common.Behaviors;

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

            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                return (TResponse)Activator.CreateInstance(
                    typeof(Result<>).MakeGenericType(typeof(TResponse).GetGenericArguments()[0]),
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    [Unknown()],
                    null)!;
            }

            throw;
        }
    }
}

public class InternalServerError
{
    public InternalServerError(string exMessage)
    {
        throw new NotImplementedException();
    }
}
