using System.Collections.Generic;
using Dietly.Domain.Enums;

namespace Dietly.WebApi.Resources.Recipes.Put.Models;

public class RecipePutRequest
{
    public int Id { get; init; }

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DifficultyLevel DifficultyLevel { get; init; }

    public string? ImageUrl { get; init; }

    public int Time { get; init; }

    public int Calories { get; init; }

    public IEnumerable<RecipePutIngredientDto> Ingredients { get; init; } = Enumerable.Empty<RecipePutIngredientDto>();

    public IEnumerable<RecipePutDirectionDto> Directions { get; init; } = Enumerable.Empty<RecipePutDirectionDto>();

    public IEnumerable<RecipePutTagDto> Tags { get; init; } = Enumerable.Empty<RecipePutTagDto>();

    public sealed record RecipePutIngredientDto(int? Id, string? Name);

    public sealed record RecipePutDirectionDto(int? Id, string Description, int Order);

    public sealed record RecipePutTagDto(int? Id, string? Name);
}
