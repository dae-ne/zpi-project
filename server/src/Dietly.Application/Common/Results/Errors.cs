using Dietly.Application.Common.Results.ErrorDefinitions;

namespace Dietly.Application.Common.Results;

public static class Errors
{
    public static ForbiddenError Forbidden(string message) => new(message);

    public static InvalidError Invalid(string message) => new(message);

    public static NotFoundError NotFound(string message) => new(message);

    public static ValidationError Validation(IEnumerable<string> errors) => new(errors);

    public static UnknownError Unknown(string message = "") => new(message);
}
