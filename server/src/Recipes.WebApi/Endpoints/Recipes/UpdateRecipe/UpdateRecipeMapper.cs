using Recipes.Application.Recipes.Commands.UpdateRecipe;

namespace Recipes.WebApi.Endpoints.Recipes.UpdateRecipe;

internal static class UpdateRecipeMapper
{
    public static UpdateRecipeCommand ToCommand(this UpdateRecipeRequest request, int userId) => new()
    {
        Id = request.Id,
        UserId = userId,
        Title = request.Title,
        Description = request.Description,
        DifficultyLevel = request.DifficultyLevel,
        ImageUrl = request.ImageUrl,
        Time = TimeSpan.FromMinutes(request.Time),
        Calories = request.Calories,
        Ingredients = request.Ingredients.Select(i => new UpdateRecipeCommand.Ingredient(i.Id, i.Name)),
        Directions = request.Directions.Select(d => new UpdateRecipeCommand.Direction(d.Id ?? 0, d.Description, d.Order)),
        Tags = request.Tags.Select(t => new UpdateRecipeCommand.Tag(t.Id, t.Name))
    };
}
