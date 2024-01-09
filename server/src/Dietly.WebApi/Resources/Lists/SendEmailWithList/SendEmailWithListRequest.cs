namespace Dietly.WebApi.Resources.Lists.SendEmailWithList;

public sealed class SendEmailWithListRequest
{
    public int UserId { get; init; }

    public string From { get; init; } = string.Empty;

    public string To { get; init; } = string.Empty;
}
