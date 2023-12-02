using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes.Data.Configurations;

internal sealed class DirectionConfiguration : IEntityTypeConfiguration<Direction>
{
    public void Configure(EntityTypeBuilder<Direction> builder)
    {
        throw new NotImplementedException();
    }
}
