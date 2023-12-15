using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest
{
    public int Id { get; init; }

    public string? UserName { get; init; }

    public string? AvatarUrl { get; init; }

    public void Deconstruct(out int id, out string? userName, out string? avatarUrl)
    {
        id = Id;
        userName = UserName;
        avatarUrl = AvatarUrl;
    }
}

[UsedImplicitly]
internal sealed class UpdateUserCommandHandler(IUserService userService, IAvatarStorage avatarStorage) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var (id, userName, avatarUrl) = request;

        var user = await userService.GetUserAsync(id, cancellationToken);

        if (!string.IsNullOrWhiteSpace(userName))
            user.UserName = userName;

        if (!string.IsNullOrWhiteSpace(avatarUrl))
            user.AvatarUrl = avatarUrl;

        await userService.UpdateUserAsync(user, cancellationToken);
    }
}
