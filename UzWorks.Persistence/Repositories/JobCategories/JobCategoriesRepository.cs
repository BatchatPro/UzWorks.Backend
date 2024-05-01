using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.JobCategories;

public class JobCategoriesRepository : GenericRepository<JobCategory>, IJobCategoriesRepository
{
    public JobCategoriesRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<JobCategory>> GetAllAsync()
    {
        return await _context.JobCategories.OrderBy(x => x.Title).ToArrayAsync();
    }

    public async Task<bool> IsExist(string jobCategoryName)
    {
        return await _context.JobCategories.AnyAsync(r => r.Title == jobCategoryName);
    }

    public async Task<bool> IsExist(Guid id)
    {
        return await _context.JobCategories.AnyAsync(r => r.Id == id);
    }
}
