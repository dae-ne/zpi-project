using Dietly.Domain.Entities;
using Dietly.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dietly.Infrastructure.Data.Configurations;

internal sealed class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Ingredients)
            .WithMany();

        builder.HasMany(r => r.Tags)
            .WithMany();

        builder.Property(recipe => recipe.Title)
            .HasMaxLength(100);

        builder.Property(recipe => recipe.Description)
            .HasMaxLength(500);
    }
}
