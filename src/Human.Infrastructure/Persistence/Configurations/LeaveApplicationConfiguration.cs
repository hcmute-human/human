using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class LeaveApplicationConfiguration : IEntityTypeConfiguration<LeaveApplication>
{
    public void Configure(EntityTypeBuilder<LeaveApplication> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.IssuerId).IsRequired();
        builder.HasOne(x => x.Issuer).WithMany().HasForeignKey(x => x.IssuerId).IsRequired();
        builder.Property(x => x.LeaveTypeId).IsRequired();
        builder.HasOne(x => x.LeaveType).WithMany().HasForeignKey(x => x.LeaveTypeId).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        builder.Property(x => x.Status).HasConversion<EnumToStringConverter<LeaveApplicationStatus>>().HasMaxLength(16);
        builder.Property(x => x.Description).HasMaxLength(128);
        builder.Property(x => x.ProcessorId);
        builder.HasOne(x => x.Processor).WithMany().HasForeignKey(x => x.ProcessorId);
    }
}
