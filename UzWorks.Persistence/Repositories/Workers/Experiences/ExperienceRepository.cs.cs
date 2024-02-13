using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.Experiences;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Workers.Experiences;

public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
{
    public ExperienceRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<Experience[]> GetAllExperiencesByWorkerIdAsync(Guid workerId)
    {
        return await _context.Experiences
            .Where(e => e.CreatedBy == workerId)
            .ToArrayAsync();
    }

    public async Task<Experience[]> GetAllExperiencesAsync()
    {
        return await _context.Experiences.ToArrayAsync();
    }
}
