using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.JobCategories;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.JobCategories;

namespace UzWorks.BL.Services.JobCategories;

public class JobCategoryService : IJobCategoryService
{
    private readonly IJobCategoriesRepository _repository;
    private readonly IMappingService _mappingService;
    
    public JobCategoryService(IJobCategoriesRepository repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<JobCategoryVM> Create(JobCategoryDto jobCategoryDto)
    {
        if (jobCategoryDto == null)
            throw new UzWorksException($"Job Category Dto can not be null.");

        var jobCategory = new JobCategory(jobCategoryDto.Title, jobCategoryDto.Description);

        await _repository.CreateAsync(jobCategory);
        await _repository.SaveChanges();

        return _mappingService.Map<JobCategoryVM, JobCategory>(jobCategory);
    }

    public async Task<IEnumerable<JobCategoryVM>> GetAllAsync()
    {
        var jobCategories = await _repository.GetAllAsync();

        var result = _mappingService.Map<IEnumerable<JobCategoryVM>, IEnumerable<JobCategory>>(jobCategories);
        return result;
    }

    public async Task<JobCategoryVM> GetById(Guid id)
    {
        var jobCategory = await _repository.GetById(id) ??
            throw new UzWorksException($"Could not find Job Category with id:  {id}");
        
        return _mappingService.Map<JobCategoryVM, JobCategory>(jobCategory);
    }

    public async Task<bool> IsExist(string jobCategoryName)
    {
        return await _repository.IsExist(jobCategoryName);
    }

    public async Task<bool> IsExist(Guid id)
    {
        return await _repository.IsExist(id);
    }

    public async Task<JobCategoryVM> Update(JobCategoryEM jobCategoryEM)
    {
        var jobCategory = await _repository.GetById(jobCategoryEM.Id) ?? 
            throw new UzWorksException($"Could not find JobCategory with Id: {jobCategoryEM.Id}");
        
        _mappingService.Map(jobCategoryEM, jobCategory);
        
        _repository.UpdateAsync(jobCategory);
        await _repository.SaveChanges();

        return _mappingService.Map<JobCategoryVM, JobCategory>(jobCategory);
    }

    public async Task<bool> Delete(Guid Id)
    {
        var jobCategory = await _repository.GetById(Id) ??
            throw new UzWorksException($"Could not find JobCategory with {Id}");
        
        _repository.Delete(jobCategory);
        await _repository.SaveChanges();

        return true;
    }
}
