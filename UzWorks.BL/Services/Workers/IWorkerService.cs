using UzWorks.Core.DataTransferObjects.Workers;

namespace UzWorks.BL.Services.Workers;

public interface IWorkerService
{

    Task<IEnumerable<WorkerVM>> GetAllAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, Guid? regionId, Guid? districtId);
    Task<WorkerVM> GetById(Guid id);
    Task<IEnumerable<WorkerVM>> GetWorkersByUserId (Guid userId);
    Task<WorkerVM> Create(WorkerDto workerDto);
    Task<WorkerVM> Update(WorkerEM workerEM);
    Task Delete(Guid id);
    Task<int> GetCount();
}
