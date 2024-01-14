namespace Dietly.Application.Common.Results.ErrorDefinitions;

public sealed class UnknownError(string message = "") : ErrorBase(message);
