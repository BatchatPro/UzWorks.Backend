using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Workers;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Workers;

namespace UzWorks.BL.Services.Workers;

public class WorkerService : IWorkerService
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IMappingService _mappingService;

    public WorkerService(IWorkersRepository workersRepository, IMappingService mappingService)
    {
        _workersRepository = workersRepository;
        _mappingService = mappingService;
    }

    public async Task<WorkerVM> Create(WorkerDto workerDto)
    {
        if (workerDto == null)
            throw new UzWorksException("Work Dto can not be null.");

        var worker = _mappingService.Map<Worker, WorkerDto>(workerDto);
        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();

        return _mappingService.Map<WorkerVM, Worker>(worker);
    }

    public async Task Delete(Guid id)
    {
        var worker = await _workersRepository.GetById(id);

        if (worker is null)
            throw new UzWorksException($"Could not find worker with id : {id}");

        _workersRepository.Delete(worker);
        await _workersRepository.SaveChanges();
    }

    public async Task<IEnumerable<WorkerVM>> GetAllAsync(int pageNumber, int pageSize, Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary, uint? minSalary, string? gender, Guid? regionId, Guid? districtId)
    {
        var workers = await _workersRepository.GetAllWorkersAsync(pageNumber, pageSize, jobCategoryId, 
                                                                maxAge, minAge, maxSalary, minSalary, 
                                                                gender, regionId, districtId);
        
        var result = _mappingService.Map<IEnumerable<WorkerVM>, IEnumerable<Worker>>(workers);
        return result;
    }

    public async Task<WorkerVM> GetById(Guid id)
    {
        var worker = await _workersRepository.GetById(id);
        
        if (worker is null) 
            throw new UzWorksException($"Could not find worker with id: {id}");
        return _mappingService.Map<WorkerVM, Worker>(worker); 
    }

    public async Task<WorkerVM> Update(WorkerEM workerEM)
    {
        if (workerEM is null)
            throw new UzWorksException("Could not be null worker edit model.");

        var worker = _mappingService.Map<Worker, WorkerEM>(workerEM);
        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();

        return _mappingService.Map<WorkerVM, Worker>(worker);
    }
}
