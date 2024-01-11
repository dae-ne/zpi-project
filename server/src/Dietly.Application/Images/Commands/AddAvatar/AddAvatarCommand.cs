using Dietly.Application.Common.Results;

namespace Dietly.Application.Images.Commands.AddAvatar;

public sealed record AddAvatarCommand(byte[] File, string FileName) : IRequest<Result<string>>;

[UsedImplicitly]
internal sealed class AddAvatarCommandHandler(IAvatarStorage storage) : IRequestHandler<AddAvatarCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        var (file, fileName) = request;
        var fileExtension = fileName.Split('.').Last();

        var newFileName = string.IsNullOrWhiteSpace(fileExtension)
            ? Guid.NewGuid().ToString()
            : $"{Guid.NewGuid()}.{fileExtension}";

        await storage.UploadAsync(newFileName, file, cancellationToken);
        return newFileName;
    }
}
