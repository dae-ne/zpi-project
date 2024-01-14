using System.Reflection;
using Dietly.Application.Common.Results;
using FluentValidation;
using Microsoft.Extensions.Logging;
using static Dietly.Application.Common.Results.Errors;

namespace Dietly.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(ILogger<TRequest> logger, IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            logger.LogWarning("No validators found for request: {RequestName}", typeof(TRequest).Name);
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        logger.LogInformation("Validating request: {RequestName}", typeof(TRequest).Name);

        var validationResults = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Count > 0)
            .SelectMany(r => r.Errors)
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                f => f.Key,
                f => f.Select(e => e.ErrorMessage).ToArray());

        if (failures.Count == 0)
        {
            logger.LogInformation("Validation successful for request: {RequestName}", typeof(TRequest).Name);
            return await next();
        }

        logger.LogWarning("Validation failed for request: {RequestName}. Errors: {@ValidationErrors}", typeof(TRequest).Name, failures);

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            return (TResponse)Activator.CreateInstance(
                typeof(Result<>).MakeGenericType(typeof(TResponse).GetGenericArguments()[0]),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                [Validation(failures)],
                null)!;
        }

        logger.LogError("The request {RequestName} does not have a Result<T> response type", typeof(TRequest).Name);
        throw new Exception($"Invalid command: {typeof(TRequest).Name}");
    }
}
