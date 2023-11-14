using UzWorks.Core.Entities.Location;

namespace UzWorks.Core.Entities.JobAndWork;

public class Worker : BaseJobEntity
{
    public string Location { get; set; }
    public DateTime BirthDate { get; set; }

    public Guid DistrictId { get; set; }
    public District District { get; set; }
    public Guid CategoryId { get; set; }
    public JobCategory JobCategory { get; set; }
}
