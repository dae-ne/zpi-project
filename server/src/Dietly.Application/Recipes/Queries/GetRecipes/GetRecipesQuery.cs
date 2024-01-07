using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Recipes.Queries.GetRecipes;

public sealed record GetRecipesQuery(int UserId) : IRequest<Result<IList<Recipe>>>;

[UsedImplicitly]
internal sealed class GetRecipesQueryHandler(IAppDbContext db) : IRequestHandler<GetRecipesQuery, Result<IList<Recipe>>>
{
    public async Task<Result<IList<Recipe>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .Where(r => r.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return recipes.Count != 0
            ? Results.Ok<IList<Recipe>>(recipes)
            : Results.NotFound<IList<Recipe>>("Recipes not found");
    }
}
