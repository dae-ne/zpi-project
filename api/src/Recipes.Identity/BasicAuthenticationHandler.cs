using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Recipes.Application.Common.Interfaces;

namespace Recipes.Identity;

public class BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAppDbContext db)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        
        try
        {
            var username = await Xd("Basic", authorizationHeader);
            var claims = new[] { new Claim("name", username), new Claim(ClaimTypes.Role, "Admin") };
            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
        catch (Exception)
        {
            Response.StatusCode = 401;
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
    
    // TODO: change name of the function
    private async Task<string> Xd(string scheme, string? header)
    {
        if (string.IsNullOrWhiteSpace(header) ||
            !header.StartsWith(scheme, StringComparison.OrdinalIgnoreCase))
        {
            throw new AuthenticationException("Invalid Authorization Header");
        }
        
        var token = header[scheme.Length..].Trim();
        var credentials = Encoding.UTF8
            .GetString(Convert.FromBase64String(token))
            .Split(':');
        
        var (username, password) = (credentials[0], credentials[1]);
        
        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new AuthenticationException("Invalid Authorization Header");
        }
        
        var user = await db.Users.SingleOrDefaultAsync(u => u.Username == username);
        
        if (user is null)
        {
            throw new AuthenticationException("Invalid Authorization Header");
        }

        if (user.Username != username || user.Password != password)
        {
            throw new AuthenticationException();
        }

        return user.Username;
    }
}
