using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human.Infrastructure.Persistence.Configurations;

public sealed class TextChoiceConfiguration : IEntityTypeConfiguration<TextChoice>
{
    public void Configure(EntityTypeBuilder<TextChoice> builder)
    {
        builder.Property(x => x.Text);
    }
}
