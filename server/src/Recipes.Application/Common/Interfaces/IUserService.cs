namespace Recipes.Application.Common.Interfaces;

public interface IUserService
{
    Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
    
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    
    Task RemoveUserAsync(int id, CancellationToken cancellationToken);
}
