using Recipes.Application.Recipes.Commands.CreateRecipe;

namespace Recipes.WebApi.Endpoints.Recipes.CreateRecipe;

internal static class CreateRecipeMapper
{
    public static CreateRecipeCommand ToCommand(this CreateRecipeRequest request, int userId) => new()
    {
        UserId = userId,
        Title = request.Title,
        Description = request.Description,
        DifficultyLevel = request.DifficultyLevel,
        ImageUrl = request.ImageUrl,
        Time = TimeSpan.FromMinutes(request.Time),
        Calories = request.Calories,
        Ingredients = request.Ingredients.Select(i => new CreateRecipeCommand.Ingredient(i.Id, i.Name)),
        Directions = request.Directions.Select(d => new CreateRecipeCommand.Direction(d.Description, d.Order)),
        Tags = request.Tags.Select(t => new CreateRecipeCommand.Tag(t.Id, t.Name))
    };
}
