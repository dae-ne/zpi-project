using Dietly.Application.Common.Interfaces;

namespace Dietly.Application.Users.Commands.RemoveUser;

public sealed record RemoveUserCommand(int UserId) : IRequest;

[UsedImplicitly]
internal sealed class RemoveUserCommandHandler(IUserService userService) : IRequestHandler<RemoveUserCommand>
{
    public async Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        await userService.RemoveUserAsync(request.UserId, cancellationToken);    
    }
}
