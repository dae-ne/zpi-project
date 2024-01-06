namespace Dietly.Application.Recipes.Commands.CreateRecipe;

internal static class CreateRecipeMapper
{
    public static Recipe ToDomain(
        this CreateRecipeCommand command,
        IEnumerable<Ingredient> ingredients,
        IEnumerable<Direction> directions,
        IEnumerable<Tag> tags) => new()
    {
        UserId = command.UserId,
        Title = command.Title,
        Description = command.Description,
        DifficultyLevel = command.DifficultyLevel,
        ImageUrl = command.ImageUrl,
        Time = command.Time,
        Calories = command.Calories,
        Ingredients = ingredients.AsQueryable(),
        Directions = directions.AsQueryable(),
        Tags = tags.AsQueryable()
    };
}
