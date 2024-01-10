using Dietly.Application.Common.Result;
using Dietly.WebApi.Resources;
using Results = Microsoft.AspNetCore.Http.Results;

namespace Dietly.WebApi.Extensions;

internal static class ResultExtensions
{
    public static IResult ToHttpResult<TValue>(this Result<TValue> result) =>
        result.ToHttpResult<TValue, TValue>(x => x);

    public static IResult ToHttpResult<TValue, TDto>(this Result<TValue> result, Func<TValue, TDto> map)
    {
        if (result.Type == ResultType.File)
        {
            return HandleFileResult(result);
        }

        var dto = result.Data is not null
            ? map(result.Data)
            : default;

        return result.Type switch
        {
            ResultType.Ok when result.Data is null => Results.Ok(),
            ResultType.Ok => Results.Ok(dto),
            ResultType.Created when dto?.GetType() == typeof(string) => Results.Created(dto as string, null),
            ResultType.Invalid => Results.Problem(statusCode: 400, detail: result.Errors.FirstOrDefault()),
            ResultType.NotFound => Results.Problem(statusCode: 404, detail: result.Errors.FirstOrDefault()),
            ResultType.Forbidden => Results.Problem(statusCode: 403, detail: result.Errors.FirstOrDefault()),
            _ => Results.Problem(statusCode: 500, detail: result.Errors.FirstOrDefault())
        };
    }

    private static IResult HandleFileResult<TValue>(Result<TValue> result)
    {
        var fileName = result.Data?.ToString();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

        var fileExtension = fileName.Split('.').Last();
        var mimeType = fileExtension switch
        {
            "png" => "image/png",
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "gif" => "image/gif",
            _ => "application/octet-stream"
        };

        var bytes = result.Data as byte[] ?? Array.Empty<byte>();
        return Results.File(bytes, mimeType);
    }
}
