using System.Collections.Generic;

namespace Dietly.WebApi.Resources;

public sealed class ErrorDto(int code, IEnumerable<string> errors)
{
    public int Code { get; init; } = code;

    public IEnumerable<string> Errors { get; init; } = errors;
}
