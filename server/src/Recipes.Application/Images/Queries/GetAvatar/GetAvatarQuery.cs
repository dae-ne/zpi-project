using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Images.Queries.GetAvatar;

public sealed record GetAvatarQuery(string FileName) : IRequest<byte[]>;

[UsedImplicitly]
internal sealed class GetAvatarQueryHandler(IAvatarStorage storage) : IRequestHandler<GetAvatarQuery, byte[]>
{
    public async Task<byte[]> Handle(GetAvatarQuery request, CancellationToken cancellationToken)
    {
        var file = await storage.GetAsync(request.FileName, cancellationToken);
        return file;
    }
}
