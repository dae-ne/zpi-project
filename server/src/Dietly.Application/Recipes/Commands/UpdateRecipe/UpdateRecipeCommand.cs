using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Recipes.Commands.UpdateRecipe;

public sealed class UpdateRecipeCommand : IRequest
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string ImageUrl { get; init; } = null!;

    public TimeSpan Time { get; init; }

    public int Calories { get; init; }

    public IEnumerable<Ingredient> Ingredients { get; init; } = Enumerable.Empty<Ingredient>();

    public IEnumerable<Direction> Directions { get; init; } = Enumerable.Empty<Direction>();

    public IEnumerable<Tag> Tags { get; init; } = Enumerable.Empty<Tag>();

    public sealed record Ingredient(int? Id, string? Name);

    public sealed record Direction(int Id, string Description, int Order);

    public sealed record Tag(int? Id, string? Name);
}

[UsedImplicitly]
internal sealed class UpdateRecipeContractHandler(IAppDbContext db) : IRequestHandler<UpdateRecipeCommand>
{
    public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var existingIngredientIds = request.Ingredients
            .Where(i => i.Id is not null)
            .Select(i => i.Id);

        var existingTagIds = request.Tags
            .Where(t => t.Id is not null)
            .Select(t => t.Id);

        var ingredients = await db.Ingredients
            .Where(i => existingIngredientIds.Contains(i.Id))
            .ToListAsync(cancellationToken);

        var tags = await db.Tags
            .Where(t => existingTagIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

        var newIngredients = request.Ingredients
            .Where(i => i.Id is null)
            .Select(i => new Ingredient
            {
                UserId = request.UserId,
                Name = i.Name!
            });

        var newTags = request.Tags
            .Where(t => t.Id is null)
            .Select(t => new Tag
            {
                UserId = request.UserId,
                Name = t.Name!
            });

        var recipe = await db.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Directions)
            .Include(r => r.Tags)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (recipe is null)
        {
            // TODO: exception type
            throw new Exception();
        }

        recipe.Title = request.Title;
        recipe.Description = request.Description;
        recipe.DifficultyLevel = request.DifficultyLevel;
        recipe.ImageUrl = request.ImageUrl;
        recipe.Time = request.Time;
        recipe.Calories = request.Calories;

        recipe.Ingredients.Clear();
        recipe.Ingredients.AddRange(ingredients);
        recipe.Ingredients.AddRange(newIngredients);

        recipe.Tags.Clear();
        recipe.Tags.AddRange(tags);
        recipe.Tags.AddRange(newTags);

        recipe.Directions.Clear();
        recipe.Directions.AddRange(request.Directions.Select(d => new Direction
        {
            Id = d.Id,
            Description = d.Description,
            Order = d.Order
        }));

        await db.SaveChangesAsync(cancellationToken);
    }
}
