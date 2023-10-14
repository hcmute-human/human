using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class UserRefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.Property<Guid>("UserId").IsRequired();
        builder.HasKey("UserId", nameof(UserRefreshToken.Token));
        builder.HasOne(x => x.User).WithMany().HasForeignKey("UserId").IsRequired();
        builder.Navigation(x => x.User).UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.ExpiryTime).IsRequired();
    }
}
