using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Dietly.Domain.Enums;
using Dietly.Domain.Events.Recipe;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Recipes.Commands.UpdateRecipe;

public sealed class UpdateRecipeCommand : IRequest<Result<object?>>
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string? ImageUrl { get; init; }

    public int Time { get; init; }

    public int Calories { get; init; }

    public IEnumerable<Ingredient> Ingredients { get; init; } = Enumerable.Empty<Ingredient>();

    public IEnumerable<Direction> Directions { get; init; } = Enumerable.Empty<Direction>();

    public IEnumerable<Tag> Tags { get; init; } = Enumerable.Empty<Tag>();

    public sealed record Ingredient(int? Id, string? Name);

    public sealed record Direction(int Id, string Description, int Order);

    public sealed record Tag(int? Id, string? Name);
}

[UsedImplicitly]
internal sealed class UpdateRecipeContractHandler(IAppDbContext db) : IRequestHandler<UpdateRecipeCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
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
            return Results.NotFound("Recipe not found");
        }

        var oldRecipe = (Recipe)recipe.Clone();

        recipe.Title = request.Title;
        recipe.Description = request.Description;
        recipe.DifficultyLevel = request.DifficultyLevel;
        recipe.ImageUrl = request.ImageUrl;
        recipe.Time = request.Time;
        recipe.Calories = request.Calories;

        foreach (var ingredient in ingredients)
        {
            var requestIngredient = request.Ingredients
                .SingleOrDefault(i => i.Id == ingredient.Id);

            if (requestIngredient is not null)
            {
                ingredient.Name = requestIngredient.Name!;
            }
        }

        var entityIngredients = new List<Ingredient>();
        entityIngredients.AddRange(ingredients);
        entityIngredients.AddRange(newIngredients);
        recipe.Ingredients = entityIngredients;

        foreach (var tag in tags)
        {
            var requestTag = request.Tags
                .SingleOrDefault(t => t.Id == tag.Id);

            if (requestTag is not null)
            {
                tag.Name = requestTag.Name!;
            }
        }

        var entityTags = new List<Tag>();
        entityTags.AddRange(tags);
        entityTags.AddRange(newTags);
        recipe.Tags = entityTags;

        var entityDirections = new List<Direction>();
        entityDirections.AddRange(request.Directions.Select(d => new Direction
        {
            Id = d.Id,
            Description = d.Description,
            Order = d.Order
        }));
        recipe.Directions = entityDirections;

        recipe.AddDomainEvent(new RecipeUpdatedEvent(oldRecipe, recipe));

        var changes = await db.SaveChangesAsync(cancellationToken);

        return changes > 0
            ? Results.Ok()
            : Results.UnknownError();
    }
}
