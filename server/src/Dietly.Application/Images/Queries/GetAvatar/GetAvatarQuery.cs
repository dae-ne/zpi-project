namespace Dietly.Application.Images.Queries.GetAvatar;

public sealed record GetAvatarQuery(string FileName) : IRequest<Result<byte[]>>;

[UsedImplicitly]
internal sealed class GetAvatarQueryHandler(IAvatarStorage storage) : IRequestHandler<GetAvatarQuery, Result<byte[]>>
{
    public async Task<Result<byte[]>> Handle(GetAvatarQuery request, CancellationToken cancellationToken)
    {
        var file = await storage.GetAsync(request.FileName, cancellationToken);
        return Results.File(file);
    }
}
