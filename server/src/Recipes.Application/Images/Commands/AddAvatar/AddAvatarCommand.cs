using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Images.Commands.AddAvatar;

public sealed record AddAvatarCommand(byte[] File, string FileName) : IRequest<string>;

[UsedImplicitly]
internal sealed class AddAvatarCommandHandler(IAvatarStorage storage) : IRequestHandler<AddAvatarCommand, string>
{
    public async Task<string> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        var (file, fileName) = request;
        var fileExtension = fileName.Split('.').Last();
        var newFileName = $"{Guid.NewGuid()}.{fileExtension}";
        await storage.UploadAsync(newFileName, file, cancellationToken);
        return newFileName;
    }
}
