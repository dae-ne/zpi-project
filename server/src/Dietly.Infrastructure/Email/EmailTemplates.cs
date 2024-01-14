namespace Dietly.Infrastructure.Email;

internal static class EmailTemplates
{
    public const string ConfirmEmail = @"
        <p>Hi {0},</p>
        <p>Thanks for signing up for Dietly! We're excited to have you as an early user.</p>
        <p>Before we get started, we just need to confirm that this is you.</p>
        <p>Click the link below to confirm your email address:</p>
        <p><a href='{1}'>Confirm email address</a></p>
        <p>Thanks,</p>
        <p>The Dietly Team</p>
    ";

    public const string GroceryList = @"
        <p>Hi {0},</p>
        <ul>
            {1}
        </ul>
        <p>Thanks,</p>
        <p>The Dietly Team</p>
    ";
}
