using UzWorks.Core.Entities.Experiences;

namespace UzWorks.Persistence.Repositories.Workers.Experiences;

public interface IExperienceRepository : IGenericRepository<Experience>
{
    Task<Experience[]> GetAllExperiencesByWorkerIdAsync(Guid workerId);
    Task<Experience[]> GetAllExperiencesAsync();
}
