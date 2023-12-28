using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.UpdatedTime).ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
        builder.Property(x => x.CandidateId);
        builder.HasOne(x => x.Candidate).WithMany().HasForeignKey(x => x.CandidateId);
        builder.Property(x => x.JobId);
        builder.HasOne(x => x.Job).WithMany().HasForeignKey(x => x.JobId);
        builder.Property(x => x.Status).HasConversion<EnumToStringConverter<JobApplicationStatus>>().HasMaxLength(16);
    }
}
