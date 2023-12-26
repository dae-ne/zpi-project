using Microsoft.Extensions.Logging;

namespace Recipes.Application.Common.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}: {@Request}", requestName, request);

        var response = await next();

        logger.LogInformation("Handled {RequestName}: {@Request} | {@Response}", requestName, request, response);

        return response;
    }
}
