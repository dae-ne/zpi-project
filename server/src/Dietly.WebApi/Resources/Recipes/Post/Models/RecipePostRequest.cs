using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Resources.Recipes.Post.Models;

public sealed class RecipePostRequest
{
    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string? ImageUrl { get; init; }

    public int Time { get; init; }

    public int Calories { get; init; }

    public IEnumerable<RecipePostIngredientDto> Ingredients { get; init; } = Enumerable.Empty<RecipePostIngredientDto>();

    public IEnumerable<RecipePostDirectionDto> Directions { get; init; } = Enumerable.Empty<RecipePostDirectionDto>();

    public IEnumerable<RecipePostTagDto> Tags { get; init; } = Enumerable.Empty<RecipePostTagDto>();

    public sealed record RecipePostIngredientDto(int? Id, string? Name);

    public sealed record RecipePostDirectionDto(string Description, int Order);

    public sealed record RecipePostTagDto(int? Id, string? Name);
}
