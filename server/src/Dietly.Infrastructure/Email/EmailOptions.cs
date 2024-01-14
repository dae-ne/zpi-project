namespace Dietly.Infrastructure.Email;

internal sealed class EmailOptions
{
    public const string Position = "Email";

    public string Host { get; init; } = null!;

    public int Port { get; init; }

    public string Username { get; init; } = null!;

    public string AppEmail { get; init; } = null!;

    public string Password { get; init; } = null!;
}
