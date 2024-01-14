namespace Dietly.Application.Common.Results.ErrorDefinitions;

public sealed class InvalidError(string message) : ErrorBase(message);
