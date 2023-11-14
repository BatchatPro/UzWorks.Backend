using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.JobCategories;

public class JobCategoriesRepository : GenericRepository<JobCategory>, IJobCategoriesRepository
{
    public JobCategoriesRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<JobCategory>> GetAllJobCategoriesAsync()
    {
        return await _context.JobCategories.ToArrayAsync();
    }

}
