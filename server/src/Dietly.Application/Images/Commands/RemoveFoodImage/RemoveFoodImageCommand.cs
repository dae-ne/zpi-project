using Dietly.Application.Common.Results;

namespace Dietly.Application.Images.Commands.RemoveFoodImage;

public sealed record RemoveFoodImageCommand(string FileName) : IRequest<Result<Unit>>;

[UsedImplicitly]
internal sealed class RemoveFoodImageCommandHandler(IImageStorage storage) : IRequestHandler<RemoveFoodImageCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveFoodImageCommand request, CancellationToken cancellationToken)
    {
        await storage.DeleteAsync(request.FileName, cancellationToken);
        return Unit.Value;
    }
}
