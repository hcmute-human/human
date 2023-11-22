using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");

        builder.Property(x => x.Name).HasMaxLength(32).IsRequired();
    }
}
