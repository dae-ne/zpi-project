using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Endpoints.Recipes.CreateRecipe;

public sealed class CreateRecipeRequest
{
    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string ImageUrl { get; init; } = null!;

    public int Time { get; init; }

    public int Calories { get; init; }

    public IEnumerable<CreateRecipeIngredientDto> Ingredients { get; init; } = Enumerable.Empty<CreateRecipeIngredientDto>();

    public IEnumerable<CreateRecipeDirectionDto> Directions { get; init; } = Enumerable.Empty<CreateRecipeDirectionDto>();

    public IEnumerable<CreateRecipeTagDto> Tags { get; init; } = Enumerable.Empty<CreateRecipeTagDto>();

    public sealed record CreateRecipeIngredientDto(int? Id, string? Name);

    public sealed record CreateRecipeDirectionDto(string Description, int Order);

    public sealed record CreateRecipeTagDto(int? Id, string? Name);
}
