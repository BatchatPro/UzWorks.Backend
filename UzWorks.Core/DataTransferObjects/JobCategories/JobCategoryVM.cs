using UzWorks.Core.DataTransferObjects.Workers;

namespace UzWorks.Core.DataTransferObjects.JobCategories;

public class JobCategoryVM
{
    public Guid? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    //public IEnumerable<WorkerVM>? Workers { get; set; }
}
