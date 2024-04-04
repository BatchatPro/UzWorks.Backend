using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzWorks.Core.Constants;
using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Persistence.EntityConfigurations;

public class WorkerEntityConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable(TableConstants.WorkersTable);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.BirthDate).IsRequired();
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
