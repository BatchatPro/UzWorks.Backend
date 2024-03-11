using UzWorks.Core.DataTransferObjects.Workers;

namespace UzWorks.BL.Services.Workers;

public interface IWorkerService
{

    Task<IEnumerable<WorkerVM>> GetAllAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, bool? status, Guid? regionId, Guid? districtId);
    Task<WorkerVM> GetById(Guid id);
    Task<IEnumerable<WorkerVM>> GetWorkersByUserId (Guid userId);
    Task<WorkerVM> Create(WorkerDto workerDto);
    Task<WorkerVM> Update(WorkerEM workerEM);
    Task<bool> ChangeStatus(Guid id, bool status);
    Task Delete(Guid id);
    Task<int> GetCount(bool? status);
    Task<int> GetCountForFilter(Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, bool? status, Guid? regionId, Guid? districtId);
}
