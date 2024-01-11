namespace Dietly.Application.Common.Results.ErrorsDefinition;

public sealed class ForbiddenError(string message) : ErrorBase(message);
