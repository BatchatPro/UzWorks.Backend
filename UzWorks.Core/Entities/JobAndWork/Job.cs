using UzWorks.Core.Entities.Location;

namespace UzWorks.Core.Entities.JobAndWork;

public class Job: BaseJobEntity
{
    public string Benefit { get; set; }
    public string Requirement { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Guid CategoryId { get; set; }
    public JobCategory? JobCategory { get; set; }

    public Guid DistrictId { get; set; }
    public District? District { get; set; }
}
