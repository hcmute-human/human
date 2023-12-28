using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.CreatorId);
        builder.HasOne(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId);
        builder.Property(x => x.Title).HasMaxLength(64).IsRequired();
        builder.Property(x => x.Description);
        builder.Property(x => x.PositionId);
        builder.HasOne(x => x.Position).WithMany().HasForeignKey(x => x.PositionId);
        builder.Property(x => x.Status).HasConversion<EnumToStringConverter<JobStatus>>().HasMaxLength(8);
    }
}
