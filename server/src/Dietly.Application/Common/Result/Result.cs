namespace Dietly.Application.Common.Result;

// TODO: refactor this
public class Result<TValue>
{
    private Result()
    {
        Type = ResultType.Ok;
        Data = default;
    }

    private Result(TValue data)
    {
        Type = ResultType.Ok;
        Data = data;
    }

    private Result(ResultType type)
    {
        Type = type;
        Data = default;
    }

    private Result(ResultType type, TValue data)
    {
        Type = type;
        Data = data;
    }

    private Result(ResultType type, IEnumerable<string> errors)
    {
        Type = type;
        Errors = errors;
        Data = default;
    }

    public ResultType Type { get; }

    public TValue? Data { get; }

    public IEnumerable<string> Errors { get; } = Enumerable.Empty<string>();

    public static Result<TValue> Ok() => new();

    public static Result<TValue> Ok(TValue data) => new(data);

    public static Result<byte[]> File(byte[] file) => new(ResultType.File, file);

    public static Result<string> Created(string data) => new(ResultType.Created, data);

    public static Result<int> Created(int data) => new(ResultType.Created, data);

    public static Result<TValue> Invalid(IEnumerable<string> errors) => new(ResultType.Invalid, errors);

    public static Result<TValue> NotFound(IEnumerable<string> errors) => new(ResultType.NotFound, errors);

    public static Result<TValue> Forbidden(IEnumerable<string> errors) => new(ResultType.Forbidden, errors);

    public static Result<TValue> ValidationError(IEnumerable<string> errors) => new(ResultType.ValidationError, errors);

    public static Result<TValue> UnknownError() => new(ResultType.UnknownError);
}
