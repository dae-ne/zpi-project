using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes.Data.Configurations;

internal sealed class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        throw new NotImplementedException();
    }
}
