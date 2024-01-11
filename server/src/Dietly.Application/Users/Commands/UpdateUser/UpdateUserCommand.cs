using Dietly.Application.Common.Exceptions;
using Dietly.Application.Common.Results;

namespace Dietly.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest<Result<Unit>>
{
    public int UserId { get; init; }

    public string UserName { get; init; } = null!;

    public string? AvatarUrl { get; init; }
}

[UsedImplicitly]
internal sealed class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToDomain();

        try
        {
            await userService.UpdateUserAsync(user, cancellationToken);
            return Unit.Value;
        }
        catch (NotFoundException ex)
        {
            return Errors.NotFound(ex.Message);
        }
    }
}
