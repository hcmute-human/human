using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.Property(x => x.UserId).IsRequired();
        builder.HasKey(x => new { x.UserId, x.Permission });
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired();
        builder.Navigation(x => x.User).UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Property(x => x.Permission).HasMaxLength(32).IsRequired();
    }
}
