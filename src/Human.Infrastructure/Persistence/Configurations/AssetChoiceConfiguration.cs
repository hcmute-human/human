using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class AssetChoiceConfiguration : IEntityTypeConfiguration<AssetChoice>
{
    public void Configure(EntityTypeBuilder<AssetChoice> builder)
    {
        builder.OwnsOne(x => x.Asset, x =>
        {
            x.Property(x => x.Key).HasMaxLength(256).IsRequired();
            x.Property(x => x.Format).HasMaxLength(12).IsRequired();
            x.Property(x => x.Version).IsRequired();
        });
    }
}
