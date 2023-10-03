using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class UserPermissionEntityTypeConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.Property<Guid>("UserId").IsRequired();
        builder.HasKey("UserId", nameof(UserPermission.Permission));
        builder.HasOne(x => x.User).WithMany().HasForeignKey("UserId").IsRequired();
        builder.Navigation(x => x.User).UsePropertyAccessMode(PropertyAccessMode.Property);
        
        builder.Property(x => x.Permission).HasMaxLength(32).IsRequired();
    }
}
