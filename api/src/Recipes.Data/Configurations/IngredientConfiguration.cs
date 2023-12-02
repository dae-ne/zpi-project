using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes.Data.Configurations;

internal sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        throw new NotImplementedException();
    }
}
