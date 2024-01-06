using Microsoft.EntityFrameworkCore;
using Recipes.Application.Common.Interfaces;
using Recipes.Domain.Enums;

namespace Recipes.Application.Recipes.Commands.CreateRecipe;

public sealed class CreateRecipeCommand : IRequest<int>
{
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

    public sealed record Direction(string Description, int Order);

    public sealed record Tag(int? Id, string? Name);
}

[UsedImplicitly]
internal sealed class CreateRecipeContractHandler(IAppDbContext db) : IRequestHandler<CreateRecipeCommand, int>
{
    public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
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

        ingredients.AddRange(newIngredients);
        tags.AddRange(newTags);

        var directions = request.Directions
            .Select(d => new Direction
            {
                Description = d.Description,
                Order = d.Order
            }).ToList();

        var recipe = request.ToDomain(ingredients, directions, tags);

        db.Recipes.Add(recipe);

        await db.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }
}
