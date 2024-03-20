using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Experiences;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Workers.Experiences;

public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
{
    public ExperienceRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<Experience[]> GetAllByWorkerIdAsync(Guid userId)
    {
        return await _context.Experiences.Where(e => e.CreatedBy == userId).OrderBy(x => x.CreateDate).ToArrayAsync();
    }

    public async Task<Experience[]> GetAllAsync()
    {
        return await _context.Experiences.OrderBy(x => x.CreateDate).ToArrayAsync();
    }
}
