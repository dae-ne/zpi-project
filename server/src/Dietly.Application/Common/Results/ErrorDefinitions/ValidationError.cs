namespace Dietly.Application.Common.Results.ErrorDefinitions;

public sealed class ValidationError(IDictionary<string, string[]> errors)
    : ErrorBase("One or more validation errors occurred.")
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}
