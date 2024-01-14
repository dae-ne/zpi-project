using Dietly.Domain.Entities;
using Dietly.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dietly.Infrastructure.Data.Configurations;

internal sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(i => i.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ingredient => ingredient.Name)
            .HasMaxLength(100);
    }
}
