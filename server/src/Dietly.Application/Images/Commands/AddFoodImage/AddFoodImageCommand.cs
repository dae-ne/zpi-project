using Dietly.Application.Common.Results;

namespace Dietly.Application.Images.Commands.AddFoodImage;

public sealed record AddFoodImageCommand(byte[] File, string FileName) : IRequest<Result<string>>;

[UsedImplicitly]
internal sealed class AddImageCommandHandler(IImageStorage storage) : IRequestHandler<AddFoodImageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddFoodImageCommand request, CancellationToken cancellationToken)
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
