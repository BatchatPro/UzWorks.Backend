using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzWorks.Core.Constants;
using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Persistence.EntityConfigurations;

public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable(TableConstants.JobsTable);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Benefit).IsRequired();
        builder.Property(x => x.Requirement).IsRequired();
        builder.Property(x => x.MinAge).IsRequired();
        builder.Property(x => x.MaxAge).IsRequired();
        builder.Property(x => x.Latitude).IsRequired();
        builder.Property(x => x.Longitude).IsRequired();
        builder.Property(x => x.Salary).IsRequired();
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.WorkingTime).IsRequired();
        builder.Property(x => x.WorkingSchedule).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Deadline).IsRequired();

        builder.HasOne(x => x.JobCategory)
            .WithMany()
                .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.JobCategory).AutoInclude();
        builder.Navigation(x => x.District).AutoInclude();
    }
}


