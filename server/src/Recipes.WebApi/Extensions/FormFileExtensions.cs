namespace Recipes.WebApi.Extensions;

internal static class FormFileExtensions
{
    public static byte[] ToByteArray(this IFormFile file)
    {
        var bytes = new byte[file.Length];
        using var stream = file.OpenReadStream();
        _ = stream.Read(bytes);
        return bytes;
    }
}
