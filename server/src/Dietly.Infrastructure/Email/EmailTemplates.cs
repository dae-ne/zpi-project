namespace Dietly.Infrastructure.Email;

internal static class EmailTemplates
{
    public const string ConfirmEmail = @"
        <div style='
            font-family: Montserrat, Arial, Helvetica, sans-serif;
            font-size: 16px;
            background-color: #f0f0f0;
            color: #000;
            padding: 16px;'
        >
            <div style='
                max-width: 400px;
                background-color: #fff;
                margin: 16px auto;
                padding: 24px;
                border-radius: 8px;
                text-align: center;'
            >
                <h1 style='font-size: 24px'>Hello</h1>
                <p style='margin-top: 16px'>
                    Thanks for signing up for Dietly!
                    Before we get started, we just need to confirm that this is you.
                    Click the button below to confirm your email address:
                </p>
                <a href='{0}' style='
                    display: inline-block;
                    border-radius: 8px;
                    text-decoration: none;
                    padding: 12px 48px;
                    margin: 8px 0;
                    background-color: #50C655;
                    color: #fff;'
                >
                    Confirm email address
                </a>
                <p>Thanks</p>
                <p style='font-style: italic;'>The Dietly Team</p>
                <img src='{1}' alt='Dietly logo' style='width: 100px; height: 100px; margin: 16px auto 0;'>
            </div>
        </div>
    ";

    public const string GroceryList = @"
        <div style='
            font-family: Montserrat, Arial, Helvetica, sans-serif;
            font-size: 16px;
            background-color: #f0f0f0;
            color: #000;
            padding: 16px;'
        >
            <div style='
                max-width: 400px;
                background-color: #fff;
                margin: 16px auto;
                padding: 24px;
                border-radius: 8px;
                text-align: center;'
            >
                <h1 style='font-size: 24px'>Hello</h1>
                <p style='margin-top: 16px'>Here is your grocery list:</p>
                <ul style='text-align: left; margin: 16px 0;'>{0}</ul>
                <img src='{1}' alt='Dietly logo' style='width: 100px; height: 100px; margin: 16px auto 0;'>
            </div>
        </div>
    ";
}
