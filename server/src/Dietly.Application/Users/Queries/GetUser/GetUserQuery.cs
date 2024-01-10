using Dietly.Application.Common.Exceptions;

namespace Dietly.Application.Users.Queries.GetUser;

public sealed record GetUserQuery(int UserId) : IRequest<Result<User>>;

[UsedImplicitly]
internal sealed class GetUserQueryHandler(IUserService userService) : IRequestHandler<GetUserQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserAsync(request.UserId, cancellationToken);
            return Results.Ok(user);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound<User>(e.Message);
        }
    }
}
