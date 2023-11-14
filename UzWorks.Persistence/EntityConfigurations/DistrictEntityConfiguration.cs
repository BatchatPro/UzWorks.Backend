using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzWorks.Core.Constants;
using UzWorks.Core.Entities.Location;

namespace UzWorks.Persistence.EntityConfigurations;

public class DistrictEntityConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(TableConstants.DistrictsTable);

        builder.Property(x => x.Name).IsRequired();

        builder.HasOne(x => x.Region)
            .WithMany()
                .HasForeignKey(x => x.RegionId)
                    .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.District)
                .HasForeignKey(x => x.DistrictId);

        builder.HasMany(x => x.Workers)
            .WithOne(x => x.District)
                .HasForeignKey(x => x.DistrictId);

        builder.Navigation(x => x.Jobs).AutoInclude();
        builder.Navigation(x => x.Workers).AutoInclude();
        builder.Navigation(x => x.Region).AutoInclude();

        builder.HasKey(x => x.Id);
    }
}
