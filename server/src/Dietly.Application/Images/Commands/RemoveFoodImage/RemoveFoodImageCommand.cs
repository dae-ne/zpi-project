using Dietly.Application.Common.Interfaces;

namespace Dietly.Application.Images.Commands.RemoveFoodImage;

public sealed record RemoveFoodImageCommand(string FileName) : IRequest;

[UsedImplicitly]
internal sealed class RemoveFoodImageCommandHandler(IImageStorage storage) : IRequestHandler<RemoveFoodImageCommand>
{
    public async Task Handle(RemoveFoodImageCommand request, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(request.FileName, cancellationToken);
    }
}