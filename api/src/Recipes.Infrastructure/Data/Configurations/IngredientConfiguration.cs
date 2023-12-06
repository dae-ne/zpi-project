using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;
using Recipes.Infrastructure.Identity;

namespace Recipes.Infrastructure.Data.Configurations;

internal sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(i => i.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
