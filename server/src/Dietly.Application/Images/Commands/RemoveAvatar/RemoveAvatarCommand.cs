using Dietly.Application.Common.Interfaces;

namespace Dietly.Application.Images.Commands.RemoveAvatar;

public sealed record RemoveAvatarCommand(string FileName) : IRequest;

[UsedImplicitly]
internal sealed class RemoveAvatarCommandHandler(IAvatarStorage storage) : IRequestHandler<RemoveAvatarCommand>
{
    public async Task Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(request.FileName, cancellationToken);
    }
}