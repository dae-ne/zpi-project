using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Resources.Recipes.GetRecipe;

public sealed class GetRecipeResponse
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string? ImageUrl { get; init; }

    public int Time { get; init; }

    public int Calories { get; init; }

    public List<GetRecipeIngredientResponse> Ingredients { get; init; } = [];

    public List<GetRecipeDirectionResponse> Directions { get; init; } = [];

    public List<GetRecipeTagResponse> Tags { get; init; } = [];

    public sealed record GetRecipeIngredientResponse(int Id, string Name);

    public sealed record GetRecipeDirectionResponse(int Id, string Description, int Order);

    public sealed record GetRecipeTagResponse(int Id, string Name);
}
