namespace Dietly.Application.Common.Result;

public static class Results
{
    public static Result<TValue> Ok<TValue>() => Result<TValue>.Ok();

    public static Result<object?> Ok() => Result<object?>.Ok();

    public static Result<TValue> Ok<TValue>(TValue data) => Result<TValue>.Ok(data);

    public static Result<byte[]> File(byte[] file) => Result<byte[]>.File(file);

    public static Result<string> Created(string data) => Result<string>.Created(data);

    public static Result<int> Created(int data) => Result<int>.Created(data);

    public static Result<TValue> Invalid<TValue>(params string[] errors) => Result<TValue>.Invalid(errors);

    public static Result<object?> Invalid(params string[] errors) => Result<object?>.Invalid(errors);

    public static Result<TValue> NotFound<TValue>(params string[] errors) => Result<TValue>.NotFound(errors);

    public static Result<object?> NotFound(params string[] errors) => Result<object?>.NotFound(errors);

    public static Result<TValue> Forbidden<TValue>(params string[] errors) => Result<TValue>.Forbidden(errors);

    public static Result<object?> Forbidden(params string[] errors) => Result<object?>.Forbidden(errors);

    public static Result<TValue> ValidationError<TValue>(params string[] errors) => Result<TValue>.ValidationError(errors);

    public static Result<object?> ValidationError(params string[] errors) => Result<object?>.ValidationError(errors);

    public static Result<TValue> UnknownError<TValue>() => Result<TValue>.UnknownError();

    public static Result<object?> UnknownError() => Result<object?>.UnknownError();
}
