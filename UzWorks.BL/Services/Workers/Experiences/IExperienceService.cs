using UzWorks.Core.DataTransferObjects.Experiences;

namespace UzWorks.BL.Services.Workers.Experiences;

public interface IExperienceService
{
    Task<IEnumerable<ExperienceVM>> GetAllExperiences();
    Task<IEnumerable<ExperienceVM>> GetExperiencesByUserId(Guid userId);
    Task<ExperienceVM> GetById(Guid id);
    Task<ExperienceVM> Create(ExperienceDto workerDto);
    Task<ExperienceVM> Update(ExperienceEM workerEM);
    Task Delete(Guid id);
}
