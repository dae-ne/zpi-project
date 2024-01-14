namespace Dietly.WebApi.Helpers;

internal static class ImageHelper
{
    public static IResult CreateHttpGetResult(byte[]? file, string fileName)
    {
        if (file is null)
        {
            return Results.NotFound("Image not found");
        }

        var fileExtension = fileName.Split('.').Last();
        var mimeType = fileExtension switch
        {
            "png" => "image/png",
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "gif" => "image/gif",
            _ => "application/octet-stream"
        };

        return Results.File(file, mimeType);
    }
}
