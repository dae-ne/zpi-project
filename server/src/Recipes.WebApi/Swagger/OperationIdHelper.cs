using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;

namespace Recipes.WebApi.Swagger;

public static class OperationIdHelper
{
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
            return relativePath.Replace("api/account/", "");
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
