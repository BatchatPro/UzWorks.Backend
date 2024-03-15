using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Feedbacks;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.FeedBacks;

public class FeedBacksRepository : GenericRepository<FeedBack>, IFeedBacksRepository
{
    public FeedBacksRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<FeedBack[]> GetAllAsync()
    {
        var query = _dbSet.Where(f => !f.IsDeleted).AsQueryable();

        return await query.ToArrayAsync();
    }
}
