using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;

namespace Dietly.Application.Images.Commands.RemoveFoodImage;

public sealed record RemoveFoodImageCommand(string FileName) : IRequest<Result<object?>>;

[UsedImplicitly]
internal sealed class RemoveFoodImageCommandHandler(IImageStorage storage) : IRequestHandler<RemoveFoodImageCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(RemoveFoodImageCommand request, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(request.FileName, cancellationToken);
        return Results.Ok();
    }
}
