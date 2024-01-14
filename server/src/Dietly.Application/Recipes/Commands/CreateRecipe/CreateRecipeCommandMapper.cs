namespace Dietly.Application.Recipes.Commands.CreateRecipe;

internal static class CreateRecipeCommandMapper
{
    public static Recipe ToDomain(
        this CreateRecipeCommand command,
        List<Ingredient> ingredients,
        List<Direction> directions,
        List<Tag> tags) => new()
    {
        UserId = command.UserId,
        Title = command.Title,
        Description = command.Description,
        DifficultyLevel = command.DifficultyLevel,
        ImageUrl = command.ImageUrl,
        Time = command.Time,
        Calories = command.Calories,
        Ingredients = ingredients,
        Directions = directions,
        Tags = tags
    };
}
