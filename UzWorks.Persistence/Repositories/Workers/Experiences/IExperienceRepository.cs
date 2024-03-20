using UzWorks.Core.Entities.Experiences;

namespace UzWorks.Persistence.Repositories.Workers.Experiences;

public interface IExperienceRepository : IGenericRepository<Experience>
{
    Task<Experience[]> GetAllByWorkerIdAsync(Guid userId);
    Task<Experience[]> GetAllAsync();
}
