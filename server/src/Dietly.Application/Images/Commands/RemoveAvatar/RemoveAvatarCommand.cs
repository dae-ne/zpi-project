using Dietly.Application.Common.Results;

namespace Dietly.Application.Images.Commands.RemoveAvatar;

public sealed record RemoveAvatarCommand(string FileName) : IRequest<Result<Unit>>;

[UsedImplicitly]
internal sealed class RemoveAvatarCommandHandler(IAvatarStorage storage) : IRequestHandler<RemoveAvatarCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(request.FileName, cancellationToken);
        return Unit.Value;
    }
}
