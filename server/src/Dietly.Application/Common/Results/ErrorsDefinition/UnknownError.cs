namespace Dietly.Application.Common.Results.ErrorsDefinition;

public sealed class UnknownError(string message = "") : ErrorBase(message);
