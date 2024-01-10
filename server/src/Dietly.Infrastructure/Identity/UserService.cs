using Dietly.Application.Common.Exceptions;
using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Entities;
using Dietly.Domain.Events.User;
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
        var appUser = await db.Users.FindAsync([user.Id], cancellationToken: cancellationToken);

        if (appUser is null)
        {
            throw new NotFoundException("User not found");
        }

        var oldAppUser = (AppUser)appUser.Clone();

        appUser.UserName = user.UserName;
        appUser.AvatarUrl = user.AvatarUrl;

        appUser.AddDomainEvent(new UserUpdatedEvent(
            oldAppUser.ToDomain(),
            appUser.ToDomain()));

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await db.Users.FindAsync([id], cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        user.AddDomainEvent(new UserRemovedEvent(user.ToDomain()));

        db.Users.Remove(user);
        await db.SaveChangesAsync(cancellationToken);
    }
}
