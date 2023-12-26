namespace Recipes.WebApi.Extensions;

internal static class HttpRequestExtensions
{
    public static string GenerateAvatarUrl(this HttpRequest request, string fileName) =>
        $"{request.Scheme}://{request.Host}/images/avatar/{fileName}";

    public static string GenerateFoodImageUrl(this HttpRequest request, string fileName) =>
        $"{request.Scheme}://{request.Host}/images/food/{fileName}";

    public static string GenerateUrlForCreatedItem(this HttpRequest request, int id) =>
        $"{request.Scheme}://{request.Host}{request.Path}/{id}";
}
