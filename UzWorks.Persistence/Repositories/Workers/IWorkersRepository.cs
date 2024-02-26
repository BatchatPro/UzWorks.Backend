using UzWorks.Core.Entities.JobAndWork;

namespace UzWorks.Persistence.Repositories.Workers;

public interface IWorkersRepository : IGenericRepository<Worker>
{
    Task<Worker[]> GetAllWorkersAsync(int pageNumber = 1, int pageSize = 15, 
                        Guid? jobCategoryId = null, int? maxAge = null, int? minAge = null, uint? maxSalary = null,
                        uint? minSalary = null, string? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null);

    Task<int> GetWorkersCount();

    Task<Worker[]> GetWorkersByUserIdAsync(Guid userId);
}
