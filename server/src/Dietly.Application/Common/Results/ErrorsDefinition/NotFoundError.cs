namespace Dietly.Application.Common.Results.ErrorsDefinition;

public sealed class NotFoundError(string message) : ErrorBase(message);
