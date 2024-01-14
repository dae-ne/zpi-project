using Dietly.Domain.Entities;
using Dietly.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dietly.Infrastructure.Data;

internal sealed class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Direction> Directions { get; set; } = null!;

    public DbSet<Ingredient> Ingredients { get; set; } = null!;

    public DbSet<Meal> Meals { get; set; } = null!;

    public DbSet<Recipe> Recipes { get; set; } = null!;

    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
