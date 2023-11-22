using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.HasOne(x => x.Department).WithMany().HasForeignKey(x => x.DepartmentId).IsRequired();
        builder.Navigation(x => x.Department).UsePropertyAccessMode(PropertyAccessMode.Property);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.Name).HasMaxLength(32).IsRequired();
    }
}
