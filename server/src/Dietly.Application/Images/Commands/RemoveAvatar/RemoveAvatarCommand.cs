namespace Dietly.Application.Images.Commands.RemoveAvatar;

public sealed record RemoveAvatarCommand(string FileName) : IRequest<Result<object?>>;

[UsedImplicitly]
internal sealed class RemoveAvatarCommandHandler(IAvatarStorage storage) : IRequestHandler<RemoveAvatarCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(request.FileName, cancellationToken);
        return Results.Ok();
    }
}
