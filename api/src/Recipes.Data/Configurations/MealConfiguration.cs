using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes.Data.Configurations;

internal sealed class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        throw new NotImplementedException();
    }
}
