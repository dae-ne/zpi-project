using Dietly.Application.Common.Exceptions;
using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Entities;
using Dietly.Infrastructure.Data;

namespace Dietly.Infrastructure.Identity;

internal sealed class UserService(AppDbContext db) : IUserService
{
    public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
        return user?.ToDomain() ?? throw new NotFoundException("User not found");
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var entity = await db.Users.FindAsync([user.Id], cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("User not found");
        }

        entity.UserName = user.UserName;
        entity.AvatarUrl = user.AvatarUrl;
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await db.Users.FindAsync([id], cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        db.Users.Remove(user);
        await db.SaveChangesAsync(cancellationToken);
    }
}
