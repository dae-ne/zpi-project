using Dietly.Application.Common.Results;

namespace Dietly.Application.Recipes.Queries.GetRecipes;

public sealed record GetRecipesQuery(int UserId) : IRequest<Result<IList<Recipe>>>;

[UsedImplicitly]
internal sealed class GetRecipesQueryHandler(IAppDbContext db) : IRequestHandler<GetRecipesQuery, Result<IList<Recipe>>>
{
    public async Task<Result<IList<Recipe>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await db.Recipes
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .Where(r => r.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return recipes.Count != 0
            ? recipes
            : Errors.NotFound("Recipes not found");
    }
}
