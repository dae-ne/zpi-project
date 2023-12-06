using Recipes.Domain.Entities;
using Recipes.Application.Common.Interfaces;
using Recipes.Infrastructure.Data;

namespace Recipes.Infrastructure.Identity;

internal sealed class UserService(AppDbContext db) : IUserService
{
    public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await db.Users.FindAsync(
            new object?[] { id },
            cancellationToken: cancellationToken);
        return user?.ToDomain() ?? throw new Exception(); // TODO: Exception type
    }
    
    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var entity = await db.Users.FindAsync(
            new object?[] { user.Id },
            cancellationToken: cancellationToken);
        
        if (entity is null)
        {
            throw new Exception(); // TODO: Exception type
        }
        
        entity.UserName = user.UserName;
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await db.Users.FindAsync(
            new object?[] { id },
            cancellationToken: cancellationToken);
        
        if (user is null)
        {
            throw new Exception(); // TODO: Exception type
        }
        
        db.Users.Remove(user);
        await db.SaveChangesAsync(cancellationToken);
    }
}
