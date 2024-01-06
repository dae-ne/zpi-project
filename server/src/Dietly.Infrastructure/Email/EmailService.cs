using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Dietly.Application.Common.Interfaces;

namespace Dietly.Infrastructure.Email;

internal sealed class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendAsync(string to, string subject, string htmlBody)
    {
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.Username, _options.AppEmail));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = htmlBody };

        using var client = new SmtpClient();
        await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_options.AppEmail, _options.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
