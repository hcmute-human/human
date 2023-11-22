using System.Security.Cryptography;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.Tokens;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class UserPasswordResetTokenConfiguration : IEntityTypeConfiguration<UserPasswordResetToken>
{
    public void Configure(EntityTypeBuilder<UserPasswordResetToken> builder)
    {
        builder.Property<Guid>("UserId").IsRequired();
        builder.HasOne(x => x.User).WithOne().HasForeignKey<UserPasswordResetToken>("UserId").IsRequired();
        builder.Navigation(x => x.User).UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.HasIndex("UserId").IsUnique();

        builder.HasKey(x => x.Token);
        builder.Property(x => x.Token)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<UserPasswordResetTokenValueGenerator>()
            .HasValueGeneratorFactory<UserPasswordResetTokenValueGeneratorFactory>();
    }
}

public sealed class UserPasswordResetTokenValueGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        return Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(32));
    }
}

public sealed class UserPasswordResetTokenValueGeneratorFactory : ValueGeneratorFactory
{
    public override ValueGenerator Create(IProperty property, ITypeBase typeBase)
    {
        return new UserPasswordResetTokenValueGenerator();
    }
}
