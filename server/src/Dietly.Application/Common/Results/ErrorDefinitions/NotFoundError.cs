namespace Dietly.Application.Common.Results.ErrorDefinitions;

public sealed class NotFoundError(string message) : ErrorBase(message);
