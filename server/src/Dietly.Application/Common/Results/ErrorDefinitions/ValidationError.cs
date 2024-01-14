namespace Dietly.Application.Common.Results.ErrorDefinitions;

public sealed class ValidationError(IEnumerable<string> errors)
    : ErrorBase("One or more validation errors occurred.")
{
    public IEnumerable<string> Errors { get; } = errors;
}
