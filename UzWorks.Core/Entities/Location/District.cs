using NullGuard;
using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Core.Entities.Location;

public class District : BaseEntity
{
    public District(string name, Guid regionId)
    {
        Name = name;
        RegionId = regionId;
    }

    public string Name { get; set; }

    public Guid RegionId { get; set; }

    [AllowNull]
    public Region? Region { get; set; }

    [AllowNull]

    public ICollection<Job>? Jobs { get; set; }
    [AllowNull]
    public ICollection<Worker>? Workers { get; set; }
}
