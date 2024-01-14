using Dietly.Application.Common.Results;

namespace Dietly.Application.Recipes.Queries.GetRecipe;

public sealed record GetRecipeQuery(int RecipeId, int UserId) : IRequest<Result<Recipe>>;

[UsedImplicitly]
internal sealed class GetRecipeQueryHandler(IAppDbContext db) : IRequestHandler<GetRecipeQuery, Result<Recipe>>
{
    public async Task<Result<Recipe>> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await db.Recipes
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .Where(r => r.UserId == request.UserId)
            .SingleOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        return recipe is not null
            ? recipe
            : Errors.NotFound("Recipe not found");
    }
}
