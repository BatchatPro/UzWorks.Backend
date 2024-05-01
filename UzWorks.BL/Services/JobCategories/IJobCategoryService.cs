using UzWorks.Core.DataTransferObjects.JobCategories;

namespace UzWorks.BL.Services.JobCategories;

public interface IJobCategoryService
{
    Task<IEnumerable<JobCategoryVM>> GetAllAsync();
    Task<JobCategoryVM> GetById(Guid id);
    Task<bool> IsExist(string jobCategroyName);
    Task<bool> IsExist(Guid categoryId);
    Task<JobCategoryVM> Create(JobCategoryDto jobCategoryDto);
    Task<bool> Delete(Guid Id);
    Task<JobCategoryVM> Update(JobCategoryEM jobCategoryEM);
}
