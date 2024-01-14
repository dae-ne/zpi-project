using Dietly.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dietly.Infrastructure.Data.Configurations;

internal sealed class DirectionConfiguration : IEntityTypeConfiguration<Direction>
{
    public void Configure(EntityTypeBuilder<Direction> builder)
    {
        builder.Property(direction => direction.Description)
            .HasMaxLength(300);

        builder.HasIndex(direction => new { direction.RecipeId, direction.Order })
            .IsUnique();
    }
}
