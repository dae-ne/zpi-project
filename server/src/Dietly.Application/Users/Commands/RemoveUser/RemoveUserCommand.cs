using Dietly.Application.Common.Exceptions;

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
