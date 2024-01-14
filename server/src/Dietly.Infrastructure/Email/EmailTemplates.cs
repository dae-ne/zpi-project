namespace Dietly.Infrastructure.Email;

internal static class EmailTemplates
{
    public const string ConfirmEmail = @"
        <h1>Confirm your email</h1>
        <p>Hi {0},</p>
        <p>Thanks for signing up for Dietly! We're excited to have you as an early user.</p>
        <p>Before we get started, we just need to confirm that this is you.</p>
        <p>Click the link below to confirm your email address:</p>
        <p><a href='{1}'>Confirm email address</a></p>
        <p>Thanks,</p>
        <p>The Dietly Team</p>
    ";

    public const string GroceryList = @"
        <h1>Here's your grocery list</h1>
        <p>Hi {0},</p>
        <p>Here's your grocery list for the week of {1}:</p>
        <ul>
            {2}
        </ul>
        <p>Thanks,</p>
        <p>The Dietly Team</p>
    ";
}
