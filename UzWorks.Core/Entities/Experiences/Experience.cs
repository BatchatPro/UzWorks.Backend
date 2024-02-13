using NullGuard;
using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Core.Entities.Experiences;

public class Experience : BaseEntity
{
    public string CompanyName { get; set; }
    
    [AllowNull]
    public string? Description { get; set; }
    
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [AllowNull]
    public Worker? Worker { get; set; }
}
