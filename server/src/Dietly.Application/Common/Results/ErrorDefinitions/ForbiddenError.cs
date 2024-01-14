namespace Dietly.Application.Common.Results.ErrorDefinitions;

public sealed class ForbiddenError(string message) : ErrorBase(message);
