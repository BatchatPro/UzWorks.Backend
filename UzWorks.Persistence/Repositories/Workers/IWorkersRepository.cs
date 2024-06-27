using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Persistence.Repositories.Workers;

public interface IWorkersRepository : IGenericRepository<Worker>
{
    Task<Worker[]> GetAllAsync(int pageNumber = 1, int pageSize = 15, 
                        Guid? jobCategoryId = null, int? maxAge = null, int? minAge = null, uint? maxSalary = null,
                        uint? minSalary = null, int? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null);

    Task<int> GetCount(bool? statusType);

    Task<int> GetCountForFilter(Guid? jobCategoryId = null, int? maxAge = null, int? minAge = null, uint? maxSalary = null,
                        uint? minSalary = null, int? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null);

    Task<Worker[]> GetByUserIdAsync(Guid userId);

    Task<int> CountOfAnnouncements(Guid userId);

    Task<Worker[]> GetTopsAsync();
}
