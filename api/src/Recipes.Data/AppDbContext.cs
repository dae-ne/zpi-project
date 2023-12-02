using Recipes.Application.Common.Interfaces;

namespace Recipes.Data;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Direction> Directions { get; set; } = null!;
    
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    
    public DbSet<Meal> Meals { get; set; } = null!;
    
    public DbSet<Recipe> Recipes { get; set; } = null!;
    
    public DbSet<Tag> Tags { get; set; } = null!;
    
    public DbSet<User> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
