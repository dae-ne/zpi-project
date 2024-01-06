using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dietly.Domain.Entities;

namespace Dietly.Infrastructure.Data.Configurations;

internal sealed class DirectionConfiguration : IEntityTypeConfiguration<Direction>
{
    public void Configure(EntityTypeBuilder<Direction> builder)
    {
        builder.Property(direction => direction.Description)
            .IsRequired();
    }
}
