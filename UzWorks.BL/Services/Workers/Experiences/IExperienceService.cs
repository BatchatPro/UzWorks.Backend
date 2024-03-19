using UzWorks.Core.DataTransferObjects.Experiences;

namespace UzWorks.BL.Services.Workers.Experiences;

public interface IExperienceService
{
    Task<IEnumerable<ExperienceVM>> GetAll();
    Task<IEnumerable<ExperienceVM>> GetByUserId(Guid userId);
    Task<ExperienceVM> GetById(Guid id);
    Task<ExperienceVM> Create(ExperienceDto workerDto);
    Task<ExperienceVM> Update(ExperienceEM workerEM);
    Task Delete(Guid id);
}
