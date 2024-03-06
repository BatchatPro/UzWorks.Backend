using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzWorks.Core.Constants;
using UzWorks.Core.Entities.Experiences;

namespace UzWorks.Persistence.EntityConfigurations;

public class ExperienceEntityConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable(TableConstants.WorkerExperiencesTable);

        builder.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Position).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();

        builder.HasKey(e => e.Id);
    }
}
