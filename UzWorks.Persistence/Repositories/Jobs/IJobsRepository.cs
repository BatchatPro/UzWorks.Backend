using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Persistence.Repositories.Jobs;

public interface IJobsRepository : IGenericRepository<Job>
{
    Task<Job[]> GetAllAsync(int pageNumber = 1, int pageSize = 15,
                        Guid? jobCategoryId = null, int? maxAge = null, int? minAge = null, uint? maxSalary = null,
                        uint? minSalary = null, string? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null);

    Task<int> GetCount(bool? statusType);

    Task<Job[]> GetByUserIdAsync(Guid userId);

    Task<int> GetcountForFilter(Guid? jobCategoryId = null, int? maxAge = null, int? minAge = null, uint? maxSalary = null,
                        uint? minSalary = null, string? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null);

    Task<Job[]> GetTopsAsync();
}
