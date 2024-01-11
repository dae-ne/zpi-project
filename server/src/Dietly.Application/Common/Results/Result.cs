namespace Dietly.Application.Common.Results;

public readonly struct Result<TData>
{
    private readonly TData? _data;
    private readonly ErrorBase? _error;

    private Result(TData data)
    {
        _data = data;
        _error = default;
        IsFailure = false;
    }

    private Result(ErrorBase error)
    {
        _data = default;
        _error = error;
        IsFailure = true;
    }

    public bool IsFailure { get; }

    public bool IsSuccess => !IsFailure;

    public static implicit operator Result<TData>(TData data) => new(data);

    public static implicit operator Result<TData>(ErrorBase error) => new(error);

    public TResult Match<TResult>(
        Func<TData, TResult> onSuccess,
        Func<ErrorBase, TResult> onFailure)
        => IsSuccess ? onSuccess(_data!) : onFailure(_error!);
}
