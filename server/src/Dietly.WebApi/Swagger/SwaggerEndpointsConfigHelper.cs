using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;

namespace Dietly.WebApi.Swagger;

public static class SwaggerEndpointsConfigHelper
{
    public static bool IsEndpointValid(string docName, ApiDescription apiDescription)
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

    public static string GetOperationId(ApiDescription apiDescription)
    {
        return apiDescription.RelativePath?.StartsWith("api/account/") == true
            ? GetAccountEndpointOperationId(apiDescription)
            : GetEndpointNameMetadataValue(apiDescription);
    }

    private static string GetEndpointNameMetadataValue(ApiDescription apiDescription)
    {
        var endpointNameMetadata = apiDescription.ActionDescriptor.EndpointMetadata
            .LastOrDefault(x => x is EndpointNameMetadata) as EndpointNameMetadata;

        if (endpointNameMetadata is null)
        {
            throw new InvalidOperationException(
                $"Unable to generate operation ID for the '{apiDescription.RelativePath}' endpoint.");
        }

        return endpointNameMetadata.EndpointName;
    }

    private static string GetAccountEndpointOperationId(ApiDescription apiDescription)
    {
        var relativePath = apiDescription.RelativePath!;

        if (!relativePath.Contains("api/account/manage"))
        {
            return relativePath.Replace("api/account/", string.Empty);
        }

        return apiDescription.RelativePath switch
        {
            "api/account/manage/2fa" => "updateTwoFactorAuthentication",
            "api/account/manage/info" when apiDescription.HttpMethod?.ToLower() == "get" => "getAccountInfo",
            "api/account/manage/info" when apiDescription.HttpMethod?.ToLower() == "post" => "updateAccountInfo",
            _ => throw new InvalidOperationException(
                "Unable to generate operation ID for the '{apiDescription.RelativePath}' endpoint.")
        };
    }
}
