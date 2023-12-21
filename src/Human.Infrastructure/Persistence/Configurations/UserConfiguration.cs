using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.Email).HasMaxLength(261).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.PasswordHash).HasMaxLength(61).IsRequired();
        builder.OwnsOne(x => x.Avatar, x =>
        {
            x.Property(x => x.Key).HasMaxLength(256).IsRequired();
            x.Property(x => x.Format).HasMaxLength(12).IsRequired();
            x.Property(x => x.Version).IsRequired();
        });
    }
}
