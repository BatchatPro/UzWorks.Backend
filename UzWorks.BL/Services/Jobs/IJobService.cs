using UzWorks.Core.DataTransferObjects.Jobs;

namespace UzWorks.BL.Services.Jobs;

public interface IJobService
{
    Task<IEnumerable<JobVM>> GetAllAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, bool? status, Guid? regionId, Guid? districtId);
    Task<JobVM> GetById(Guid id);
    Task<IEnumerable<JobVM>> GetByUserId(Guid userId);
    Task<IEnumerable<JobVM>> GetTops();
    Task<JobVM> Create(JobDto jobDto);
    Task<JobVM> Update(JobEM jobEM);
    Task<bool> ChangeStatus(Guid id, bool status);
    Task<bool> Delete(Guid id);
    Task<int> GetCount(bool? status);
    Task<int> GetGountForFilter(Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, bool? status, Guid? regionId, Guid? districtId);
}
