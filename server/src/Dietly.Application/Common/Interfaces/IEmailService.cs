using Dietly.Application.Common.Enums;

namespace Dietly.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string content, EmailTemplate template = EmailTemplate.None);
}
