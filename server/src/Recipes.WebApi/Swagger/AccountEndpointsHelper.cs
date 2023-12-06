using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Recipes.WebApi.Swagger;

internal static class AccountEndpointsHelper
{
    public static bool IsEndpointValid(string _, ApiDescription apiDescription)
    {
        var pathsToExclude = new[]
        {
            // "api/account/register",
            // "api/account/login",
            // "api/account/refresh",
            // "api/account/confirmEmail",
            // "api/account/resendConfirmationEmail",
            "api/account/forgotPassword",
            "api/account/resetPassword",
            "api/account/manage/2fa",
            // "api/account/manage/info"
        };
        var path = apiDescription.RelativePath;
            
        return !pathsToExclude.Any(p => path!.Contains(p));
    }
}
