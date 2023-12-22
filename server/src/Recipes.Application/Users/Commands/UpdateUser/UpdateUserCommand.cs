using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest
{
    public int UserId { get; init; }

    public string UserName { get; init; } = null!;

    public string? AvatarUrl { get; init; }
}

[UsedImplicitly]
internal sealed class UpdateUserCommandHandler(IUserService userService, IAvatarStorage avatarStorage) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToDomain();
        await userService.UpdateUserAsync(user, cancellationToken);
    }
}
