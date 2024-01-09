using Dietly.Application.Common.Exceptions;
using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;

namespace Dietly.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest<Result<object?>>
{
    public int UserId { get; init; }

    public string UserName { get; init; } = null!;

    public string? AvatarUrl { get; init; }
}

[UsedImplicitly]
internal sealed class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToDomain();

        try
        {
            await userService.UpdateUserAsync(user, cancellationToken);
            return Results.Ok();
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
}
