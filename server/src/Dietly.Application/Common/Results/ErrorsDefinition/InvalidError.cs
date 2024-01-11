namespace Dietly.Application.Common.Results.ErrorsDefinition;

public sealed class InvalidError(string message) : ErrorBase(message);
