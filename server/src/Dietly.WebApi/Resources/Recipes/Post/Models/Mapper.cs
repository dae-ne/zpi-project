using Dietly.Application.Recipes.Commands.CreateRecipe;

namespace Dietly.WebApi.Resources.Recipes.Post.Models;

internal static class Mapper
{
    public static CreateRecipeCommand ToCommand(this RecipePostRequest request, int userId) => new()
    {
        UserId = userId,
        Title = request.Title,
        Description = request.Description,
        DifficultyLevel = request.DifficultyLevel,
        ImageUrl = request.ImageUrl,
        Time = request.Time,
        Calories = request.Calories,
        Ingredients = request.Ingredients.Select(i => new CreateRecipeCommand.Ingredient(i.Id, i.Name)),
        Directions = request.Directions.Select(d => new CreateRecipeCommand.Direction(d.Description, d.Order)),
        Tags = request.Tags.Select(t => new CreateRecipeCommand.Tag(t.Id, t.Name))
    };
}
