using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Experiences;
using UzWorks.Core.Entities.Experiences;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Workers.Experiences;

namespace UzWorks.BL.Services.Workers.Experiences;

public class ExperienceService : IExperienceService
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IMappingService _mappingService;
    private readonly IEnvironmentAccessor _environmentAccessor;

    public ExperienceService(
        IExperienceRepository experienceRepository, 
        IMappingService mappingService, 
        IEnvironmentAccessor environmentAccessor
        )
    {
        _experienceRepository = experienceRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor;
    }

    public async Task<ExperienceVM> Create(ExperienceDto workerDto)
    {
        if (workerDto == null)
            throw new UzWorksException("Work Dto can not be null.");

        var experience = _mappingService.Map<Experience, ExperienceDto>(workerDto);

        experience.CreateDate = DateTime.Now;
        experience.CreatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        await _experienceRepository.CreateAsync(experience);
        await _experienceRepository.SaveChanges();

        return _mappingService.Map<ExperienceVM, Experience>(experience);
    }

    public async Task Delete(Guid id)
    {
        var experience = await _experienceRepository.GetById(id);

        if (experience is null)
            throw new UzWorksException($"Could not find experience with id : {id}");

        if (!_environmentAccessor.GetUserId().Equals(experience.CreatedBy))
            throw new UzWorksException("You have not access for delete this Experience.");

        _experienceRepository.Delete(experience);
        await _experienceRepository.SaveChanges();
    }

    public async Task<IEnumerable<ExperienceVM>> GetAllExperiences()
    {
        var experiences = await _experienceRepository.GetAllExperiencesAsync();
        experiences = experiences.OrderByDescending(x => x.CreateDate).ToArray();
        
        return _mappingService.Map<IEnumerable<ExperienceVM>, IEnumerable<Experience>>(experiences);
    }

    public async Task<ExperienceVM> GetById(Guid id)
    {
        var experience = await _experienceRepository.GetById(id);

        if (experience is null)
            throw new UzWorksException($"Could not find experience with id : {id}");

        return _mappingService.Map<ExperienceVM, Experience>(experience);
    }

    public async Task<IEnumerable<ExperienceVM>> GetExperiencesByUserId(Guid userId)
    {
        var experiences = await _experienceRepository.GetAllExperiencesByWorkerIdAsync(userId);
        experiences = experiences.OrderByDescending(x => x.CreateDate).ToArray();
        
        return _mappingService.Map<IEnumerable<ExperienceVM>, IEnumerable<Experience>>(experiences);
    }

    public async Task<ExperienceVM> Update(ExperienceEM workerEM)
    {
        var experience = _mappingService.Map<Experience, ExperienceEM>(workerEM);

        experience.UpdateDate = DateTime.Now;
        experience.UpdatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        if (_environmentAccessor.GetUserId() != experience.CreatedBy.ToString())
            throw new UzWorksException("You have not access for update this Experience.");

        _experienceRepository.UpdateAsync(experience);
        await _experienceRepository.SaveChanges();

        return _mappingService.Map<ExperienceVM, Experience>(experience);
    }
}
