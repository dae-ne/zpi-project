using Dietly.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Recipes.Commands.RemoveRecipe;

public sealed record RemoveRecipeCommand(int RecipeId) : IRequest;

[UsedImplicitly]
internal sealed class RemoveRecipeCommandHandler(IAppDbContext db) : IRequestHandler<RemoveRecipeCommand>
{
    public async Task Handle(RemoveRecipeCommand request, CancellationToken cancellationToken)
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

        db.Recipes.Remove(recipe);
        await db.SaveChangesAsync(cancellationToken);
    }
}
