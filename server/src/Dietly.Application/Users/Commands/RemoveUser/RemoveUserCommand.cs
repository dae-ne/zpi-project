using Dietly.Application.Common.Exceptions;
using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;

namespace Dietly.Application.Users.Commands.RemoveUser;

public sealed record RemoveUserCommand(int UserId) : IRequest<Result<object?>>;

[UsedImplicitly]
internal sealed class RemoveUserCommandHandler(IUserService userService) : IRequestHandler<RemoveUserCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userService.RemoveUserAsync(request.UserId, cancellationToken);
            return Results.Ok();
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }
}
