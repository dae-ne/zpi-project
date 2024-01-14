using Dietly.Application.Common.Results;
using Dietly.Domain.Events.Recipe;

namespace Dietly.Application.Recipes.Commands.RemoveRecipe;

public sealed record RemoveRecipeCommand(int RecipeId, int UserId) : IRequest<Result<Unit>>;

[UsedImplicitly]
internal sealed class RemoveRecipeCommandHandler(IAppDbContext db) : IRequestHandler<RemoveRecipeCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .Where(r => r.UserId == request.UserId)
            .SingleOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        if (recipe is null)
        {
            return Errors.NotFound("Recipe not found");
        }

        recipe.AddDomainEvent(new RecipeRemovedEvent(recipe));

        db.Recipes.Remove(recipe);
        var changes = await db.SaveChangesAsync(cancellationToken);

        return changes > 0
            ? Unit.Value
            : Errors.Unknown();
    }
}
