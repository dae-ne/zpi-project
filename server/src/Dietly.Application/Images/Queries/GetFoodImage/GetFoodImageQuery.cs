using Dietly.Application.Common.Interfaces;

namespace Dietly.Application.Images.Queries.GetFoodImage;

public sealed record GetFoodImageQuery(string FileName) : IRequest<byte[]>;

[UsedImplicitly]
internal sealed class GetFoodImageQueryHandler(IImageStorage storage) : IRequestHandler<GetFoodImageQuery, byte[]>
{
    public async Task<byte[]> Handle(GetFoodImageQuery request, CancellationToken cancellationToken)
    {
        var file = await storage.GetAsync(request.FileName, cancellationToken);
        return file;
    }
}
