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
        var experience = _mappingService.Map<Experience, ExperienceDto>(workerDto) ??
            throw new UzWorksException("Could not map ExperienceDto to Experience.");

        experience.CreateDate = DateTime.Now;
        experience.CreatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        await _experienceRepository.CreateAsync(experience);
        await _experienceRepository.SaveChanges();

        return _mappingService.Map<ExperienceVM, Experience>(experience);
    }

    public async Task<IEnumerable<ExperienceVM>> GetAll()
    {
        var experiences = await _experienceRepository.GetAllAsync();
        
        return _mappingService.Map<IEnumerable<ExperienceVM>, IEnumerable<Experience>>(experiences);
    }

    public async Task<ExperienceVM> GetById(Guid id)
    {
        var experience = await _experienceRepository.GetById(id) ?? 
            throw new UzWorksException($"Could not find experience with id : {id}");

        return _mappingService.Map<ExperienceVM, Experience>(experience);
    }

    public async Task<IEnumerable<ExperienceVM>> GetByUserId(Guid userId)
    {
        var experiences = await _experienceRepository.GetAllByWorkerIdAsync(userId);
        
        return _mappingService.Map<IEnumerable<ExperienceVM>, IEnumerable<Experience>>(experiences);
    }

    public async Task<ExperienceVM> Update(ExperienceEM experienceEM)
    {
        var experience = await _experienceRepository.GetById(experienceEM.Id) ?? 
            throw new UzWorksException($"Could not find experience with {experienceEM.Id}.");

        if (!_environmentAccessor.IsAuthorOrAdmin(Guid.Parse(_environmentAccessor.GetUserId())))
            throw new UzWorksException("You have not access for update this Experience.");

        _mappingService.Map(experienceEM, experience);

        experience.UpdateDate = DateTime.Now;
        experience.UpdatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        _experienceRepository.UpdateAsync(experience);
        await _experienceRepository.SaveChanges();

        return _mappingService.Map<ExperienceVM, Experience>(experience);
    }

    public async Task<bool> Delete(Guid id)
    {
        var experience = await _experienceRepository.GetById(id) ??
            throw new UzWorksException($"Could not find experience with id : {id}");

        if (_environmentAccessor.IsAuthorOrAdmin(Guid.Parse(_environmentAccessor.GetUserId())))
            throw new UzWorksException("You have not access for delete this Experience.");

        _experienceRepository.Delete(experience);
        await _experienceRepository.SaveChanges();

        return true;
    }
}
