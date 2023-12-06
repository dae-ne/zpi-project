namespace Recipes.Infrastructure.Blob;

internal sealed class ImageStorageOptions
{
    public const string Position = "ImageStorage";
    
    public string AvatarContainerName { get; init; } = null!;
    
    public string ImageContainerName { get; init; } = null!;
}
