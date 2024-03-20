using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Persistence.Repositories.JobCategories;

public interface IJobCategoriesRepository : IGenericRepository<JobCategory>
{
    Task<IEnumerable<JobCategory>> GetAllAsync();
}
