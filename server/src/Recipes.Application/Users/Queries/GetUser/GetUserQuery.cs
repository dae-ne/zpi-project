using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Users.Queries.GetUser;

public sealed record GetUserQuery(int Id) : IRequest<User>;

[UsedImplicitly]
internal sealed class GetUserQueryHandler(IUserService userService) : IRequestHandler<GetUserQuery, User>
{
    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userId = request.Id;
        var user = await userService.GetUserAsync(userId, cancellationToken);
        return user;
    }
}
