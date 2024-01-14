namespace Dietly.Application.Common.Results;

public abstract class ErrorBase(string message)
{
    public string Message { get; } = message;
}
