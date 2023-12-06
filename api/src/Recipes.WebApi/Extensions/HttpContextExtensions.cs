namespace Recipes.WebApi.Extensions;

internal static class HttpContextExtensions
{
    public static string GenerateAvatarUrl(this HttpContext httpContext, string fileName) =>
        $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/avatar/{fileName}";

    public static string GenerateFoodImageUrl(this HttpContext httpContext, string fileName) =>
        $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/food/{fileName}";
}
