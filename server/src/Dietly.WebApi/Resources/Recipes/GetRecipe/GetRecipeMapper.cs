using Dietly.Domain.Entities;

namespace Dietly.WebApi.Resources.Recipes.GetRecipe;

internal static class GetRecipeMapper
{
    public static GetRecipeResponse ToDto(this Recipe recipe)
    {
        var response = new GetRecipeResponse
        {
            Id = recipe.Id,
            UserId = recipe.UserId,
            Title = recipe.Title,
            Description = recipe.Description,
            DifficultyLevel = recipe.DifficultyLevel,
            ImageUrl = recipe.ImageUrl,
            Time = recipe.Time,
            Calories = recipe.Calories
        };

        response.Ingredients.AddRange(recipe.Ingredients.Select(i => new GetRecipeResponse.GetRecipeIngredientResponse(i.Id, i.Name)));
        response.Directions.AddRange(recipe.Directions.Select(d => new GetRecipeResponse.GetRecipeDirectionResponse(d.Id, d.Description, d.Order)));
        response.Tags.AddRange(recipe.Tags.Select(t => new GetRecipeResponse.GetRecipeTagResponse(t.Id, t.Name)));

        return response;
    }
}
