using Dietly.Application.Common.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dietly.Infrastructure.Email;

internal sealed class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    public async Task SendAsync(string to, string subject, string content, EmailTemplate template = EmailTemplate.None)
    {
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(options.Value.Username, options.Value.AppEmail));
        message.To.Add(new MailboxAddress(string.Empty, to));
        message.Subject = subject;
        var builder = new BodyBuilder();
        var logoUrl = options.Value.LogoUrl;

        switch (template)
        {
            case EmailTemplate.EmailConfirmation:
                builder.HtmlBody = string.Format(EmailTemplates.ConfirmEmail, content, logoUrl);
                builder.TextBody = "-";
                message.Body = builder.ToMessageBody();
                break;
            case EmailTemplate.GroceryList:
                builder.HtmlBody = string.Format(EmailTemplates.GroceryList, content, logoUrl);
                builder.TextBody = "-";
                message.Body = builder.ToMessageBody();
                break;
            case EmailTemplate.None:
                message.Body = new TextPart("plain") { Text = content };
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(template), template, null);
        }

        using var client = new SmtpClient();
        await client.ConnectAsync(options.Value.Host, options.Value.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(options.Value.AppEmail, options.Value.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
