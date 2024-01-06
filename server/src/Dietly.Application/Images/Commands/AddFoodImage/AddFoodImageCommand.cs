using Dietly.Application.Common.Interfaces;

namespace Dietly.Application.Images.Commands.AddFoodImage;

public sealed record AddFoodImageCommand(byte[] File, string FileName) : IRequest<string>;

[UsedImplicitly]
internal sealed class AddImageCommandHandler(IImageStorage storage) : IRequestHandler<AddFoodImageCommand, string>
{
    public async Task<string> Handle(AddFoodImageCommand request, CancellationToken cancellationToken)
    {
        var (file, fileName) = request;
        var fileExtension = fileName.Split('.').Last();
        var newFileName = $"{Guid.NewGuid()}.{fileExtension}";
        await storage.UploadAsync(newFileName, file, cancellationToken);
        return newFileName;
    }
}
