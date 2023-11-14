using NullGuard;

namespace UzWorks.Core.Entities.JobAndWork;

public class JobCategory : BaseEntity
{
    public string Title { get; set; }

    [AllowNull]
    public string? Description { get; set; }

    [AllowNull]
    public List<Job>? Jobs{ get; set; }

    [AllowNull]
    public List<Worker>? Works { get; set; }

}