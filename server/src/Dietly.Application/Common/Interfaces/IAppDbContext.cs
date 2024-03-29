﻿namespace Dietly.Application.Common.Interfaces;

public interface IAppDbContext
{
    public DbSet<Direction> Directions { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<Meal> Meals { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Tag> Tags { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
