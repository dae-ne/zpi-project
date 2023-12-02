using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        throw new NotImplementedException();
    }
}
