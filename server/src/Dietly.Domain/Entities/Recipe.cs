using Dietly.Domain.Common;
using Dietly.Domain.Enums;

namespace Dietly.Domain.Entities;

public class Recipe : BaseCloneableEntity
{
    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }

    public string? ImageUrl { get; set; }

    public TimeSpan Time { get; set; }

    public int Calories { get; set; }

    public IQueryable<Ingredient> Ingredients { get; set; } = Enumerable.Empty<Ingredient>().AsQueryable();

    public IQueryable<Direction> Directions { get; set; } = Enumerable.Empty<Direction>().AsQueryable();

    public IQueryable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>().AsQueryable();
}
