using Dietly.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Recipes.Queries.GetRecipe;

public sealed record GetRecipeQuery(int RecipeId, int UserId) : IRequest<Recipe>;

[UsedImplicitly]
internal sealed class GetRecipeQueryHandler(IAppDbContext db) : IRequestHandler<GetRecipeQuery, Recipe>
{
    public async Task<Recipe> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .SingleOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        if (recipe is null)
        {
            // TODO: handle not found
        }

        if (recipe.UserId != request.UserId)
        {
            // TODO: handle unauthorized
        }
        
        return recipe;
    }
}
