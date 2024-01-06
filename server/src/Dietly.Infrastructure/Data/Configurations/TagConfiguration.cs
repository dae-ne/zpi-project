using Dietly.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dietly.Domain.Entities;

namespace Dietly.Infrastructure.Data.Configurations;

internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
