using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dietly.Domain.Entities;

namespace Dietly.Infrastructure.Data.Configurations;

internal sealed class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {

    }
}
