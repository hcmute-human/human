using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithOne().HasForeignKey<Employee>(x => x.Id);
        builder.Navigation(x => x.User).UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.FirstName).HasMaxLength(32).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(32).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.Gender).IsRequired().HasConversion<EnumToStringConverter<Gender>>();
    }
}
