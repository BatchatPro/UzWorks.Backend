using NullGuard;

namespace UzWorks.Core.Entities.JobAndWork;

public class JobCategory : BaseEntity
{
    public JobCategory(string title)
    {
        Title = title;
    }

    public JobCategory(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; set; }

    [AllowNull]
    public string? Description { get; set; }

    [AllowNull]
    public List<Job>? Jobs{ get; set; }

    [AllowNull]
    public List<Worker>? Works { get; set; }

}