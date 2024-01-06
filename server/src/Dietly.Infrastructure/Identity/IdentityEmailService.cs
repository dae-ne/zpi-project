using Dietly.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Dietly.Infrastructure.Identity;

internal sealed class IdentityEmailService(IEmailService emailService) : IEmailSender<AppUser>
{
    public async Task SendConfirmationLinkAsync(AppUser user, string email, string confirmationLink)
    {
        // TODO: There's something wrong with the generated link. Here's a workaround, but it's far from perfect.
        var fixedLink = confirmationLink.Replace("&amp;code", "&code");
        await emailService.SendAsync(email, "Confirm your email", fixedLink);
    }

    public async Task SendPasswordResetLinkAsync(AppUser user, string email, string resetLink)
    {
        await emailService.SendAsync(email, "Reset your password", resetLink);
    }

    public async Task SendPasswordResetCodeAsync(AppUser user, string email, string resetCode)
    {
        await emailService.SendAsync(email, "Reset your password", resetCode);
    }
}
