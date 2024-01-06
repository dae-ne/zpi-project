using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Endpoints.Recipes.GetRecipe;

public sealed class GetRecipeResponse
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string ImageUrl { get; init; } = null!;

    public int Time { get; init; }

    public int Calories { get; init; }

    public List<GetRecipeIngredientResponse> Ingredients { get; init; } = new();

    public List<GetRecipeDirectionResponse> Directions { get; init; } = new();

    public List<GetRecipeTagResponse> Tags { get; init; } = new();

    public sealed record GetRecipeIngredientResponse(int Id, string Name);

    public sealed record GetRecipeDirectionResponse(int Id, string Description, int Order);

    public sealed record GetRecipeTagResponse(int Id, string Name);
}
