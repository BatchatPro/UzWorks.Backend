using NullGuard;
using UzWorks.Core.Entities.Experiences;
using UzWorks.Core.Entities.Location;
using UzWorks.Core.Entities.Skills;

namespace UzWorks.Core.Entities.JobAndWork;

public class Worker : BaseJobEntity
{
    public DateTime BirthDate { get; set; }
    public string Location { get; set; }
    [AllowNull]
    public string? AboutMe { get; set; } = string.Empty;

    public Guid DistrictId { get; set; }
    public District District { get; set; }
    public Guid CategoryId { get; set; }
    public JobCategory JobCategory { get; set; }

    [AllowNull]
    public List<Skill>? Skills { get; set; }
    
    [AllowNull]
    public List<Experience>? Experiences { get; set; }
}
