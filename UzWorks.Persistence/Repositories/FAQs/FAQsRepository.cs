using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.FAQs;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.FAQs;

public class FAQsRepository : GenericRepository<FAQ>, IFAQsRepository
{
    public FAQsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<FAQ[]> GetAllFAQsAsync()
    {
        var query = _dbSet.Where(f => !f.IsDeleted).AsQueryable();

        return await query.ToArrayAsync();
    }
}
