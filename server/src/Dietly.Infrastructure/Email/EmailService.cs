using Dietly.Application.Common.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dietly.Infrastructure.Email;

internal sealed class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendAsync(string to, string subject, string content, EmailTemplate template = EmailTemplate.None)
    {
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.Username, _options.AppEmail));
        message.To.Add(new MailboxAddress(string.Empty, to));
        message.Subject = subject;
        var builder = new BodyBuilder();

        switch (template)
        {
            case EmailTemplate.EmailConfirmation:
                builder.HtmlBody = EmailTemplates.ConfirmEmail
                    .Replace("{0}", to)
                    .Replace("{1}", content);
                builder.TextBody = "-";
                message.Body = builder.ToMessageBody();
                break;
            case EmailTemplate.GroceryList:
                builder.HtmlBody = EmailTemplates.GroceryList
                    .Replace("{0}", to)
                    .Replace("{1}", content);
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
        await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_options.AppEmail, _options.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
