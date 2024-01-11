using Dietly.Application.Common.Exceptions;
using Dietly.Application.Common.Results;

namespace Dietly.Application.Users.Commands.RemoveUser;

public sealed record RemoveUserCommand(int UserId) : IRequest<Result<Unit>>;

[UsedImplicitly]
internal sealed class RemoveUserCommandHandler(IUserService userService) : IRequestHandler<RemoveUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userService.RemoveUserAsync(request.UserId, cancellationToken);
            return Unit.Value;
        }
        catch (NotFoundException e)
        {
            return Errors.NotFound(e.Message);
        }
    }
}
