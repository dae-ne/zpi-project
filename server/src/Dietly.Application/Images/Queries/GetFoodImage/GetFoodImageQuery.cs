using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;

namespace Dietly.Application.Images.Queries.GetFoodImage;

public sealed record GetFoodImageQuery(string FileName) : IRequest<Result<byte[]>>;

[UsedImplicitly]
internal sealed class GetFoodImageQueryHandler(IImageStorage storage) : IRequestHandler<GetFoodImageQuery, Result<byte[]>>
{
    public async Task<Result<byte[]>> Handle(GetFoodImageQuery request, CancellationToken cancellationToken)
    {
        var file = await storage.GetAsync(request.FileName, cancellationToken);
        return Results.File(file);
    }
}
