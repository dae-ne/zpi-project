using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Resources.Recipes.Get.Models;

public sealed class RecipeGetResponse
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string? ImageUrl { get; init; }

    public int Time { get; init; }

    public int Calories { get; init; }

    public List<RecipeGetIngredientDto> Ingredients { get; init; } = [];

    public List<RecipeGetDirectionDto> Directions { get; init; } = [];

    public List<RecipeGetTagDto> Tags { get; init; } = [];

    public sealed record RecipeGetIngredientDto(int Id, string Name);

    public sealed record RecipeGetDirectionDto(int Id, string Description, int Order);

    public sealed record RecipeGetTagDto(int Id, string Name);
}
