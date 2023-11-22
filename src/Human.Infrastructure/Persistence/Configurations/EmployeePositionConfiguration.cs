using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.HasKey(x => new { x.EmployeeId, x.DepartmentPositionId });
        builder.HasOne(x => x.Employee).WithMany(x => x.Positions).HasForeignKey(x => x.EmployeeId);
        builder.Navigation(x => x.Employee).UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.HasOne(x => x.DepartmentPosition).WithMany().HasForeignKey(x => x.DepartmentPositionId);
        builder.Navigation(x => x.DepartmentPosition).UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        builder.Property(x => x.EmploymentType).HasConversion<EnumToStringConverter<EmploymentType>>().IsRequired();
        builder.Property(x => x.Salary).IsRequired();
    }
}
