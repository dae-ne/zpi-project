using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Resources.Recipes.UpdateRecipe;

public class UpdateRecipeRequest
{
    public int Id { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string? ImageUrl { get; init; }

    public int Time { get; init; }

    public int Calories { get; init; }

    public IEnumerable<UpdateRecipeIngredientDto> Ingredients { get; init; } = Enumerable.Empty<UpdateRecipeIngredientDto>();

    public IEnumerable<UpdateRecipeDirectionDto> Directions { get; init; } = Enumerable.Empty<UpdateRecipeDirectionDto>();

    public IEnumerable<UpdateRecipeTagDto> Tags { get; init; } = Enumerable.Empty<UpdateRecipeTagDto>();

    public sealed record UpdateRecipeIngredientDto(int? Id, string? Name);

    public sealed record UpdateRecipeDirectionDto(int? Id, string Description, int Order);

    public sealed record UpdateRecipeTagDto(int? Id, string? Name);
}
