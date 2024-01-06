using Dietly.Application.Common.Interfaces;

namespace Dietly.Application.Users.Queries.GetUser;

public sealed record GetUserQuery(int UserId) : IRequest<User>;

[UsedImplicitly]
internal sealed class GetUserQueryHandler(IUserService userService) : IRequestHandler<GetUserQuery, User>
{
    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserAsync(request.UserId, cancellationToken);
        return user;
    }
}
