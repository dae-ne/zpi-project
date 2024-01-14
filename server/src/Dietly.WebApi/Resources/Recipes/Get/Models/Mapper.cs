using System.Collections.Generic;
using Dietly.Domain.Entities;

namespace Dietly.WebApi.Resources.Recipes.Get.Models;

internal static class Mapper
{
    public static RecipeGetResponse ToDto(this Recipe recipe)
    {
        var response = new RecipeGetResponse
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

        response.Ingredients.AddRange(recipe.Ingredients.Select(i => new RecipeGetResponse.RecipeGetIngredientDto(i.Id, i.Name)));
        response.Directions.AddRange(recipe.Directions.Select(d => new RecipeGetResponse.RecipeGetDirectionDto(d.Id, d.Description, d.Order)));
        response.Tags.AddRange(recipe.Tags.Select(t => new RecipeGetResponse.RecipeGetTagDto(t.Id, t.Name)));

        return response;
    }

    public static RecipesGetResponse ToDto(this IList<Recipe> recipes) => new()
    {
        Count = recipes.Count,
        Data = recipes.Select(r => r.ToDto())
    };
}
