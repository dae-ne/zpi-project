using Microsoft.EntityFrameworkCore;
using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Recipes.Queries.GetRecipes;

public sealed record GetRecipesQuery(int UserId) : IRequest<IList<Recipe>>;

[UsedImplicitly]
internal sealed class GetRecipesQueryHandler(IAppDbContext db) : IRequestHandler<GetRecipesQuery, IList<Recipe>>
{
    public async Task<IList<Recipe>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .ToListAsync(cancellationToken);
        
        var userIds = recipes
            .Select(r => r.UserId)
            .Distinct()
            .ToList();

        if (userIds.Count != 1)
        {
            // TODO: handle this case
        }

        if (userIds.Single() != request.UserId)
        {
            // TODO: handle this case
        }

        return recipes;
    }
}
