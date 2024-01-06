using System.Security.Authentication;
using System.Security.Claims;

namespace Dietly.WebApi.Services;

public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor)
{
    public int GetId()
    {
        var id = httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (id is null)
        {
            throw new AuthenticationException("User is not authenticated.");
        }
        
        return int.Parse(id);
    }
}
