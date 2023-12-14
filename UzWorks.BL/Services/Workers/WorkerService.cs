using System.Text;
using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.DataTransferObjects.Workers;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Workers;

namespace UzWorks.BL.Services.Workers;

public class WorkerService : IWorkerService
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IMappingService _mappingService;
    private readonly IEnvironmentAccessor _environmentAccessor;

    public WorkerService(IWorkersRepository workersRepository, IMappingService mappingService, IEnvironmentAccessor environmentAccessor)
    {
        _workersRepository = workersRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor;
    }

    public async Task<WorkerVM> Create(WorkerDto workerDto)
    {
        if (workerDto == null)
            throw new UzWorksException("Work Dto can not be null.");

        var worker = _mappingService.Map<Worker, WorkerDto>(workerDto);
        
        worker.CreateDate = DateTime.Now;
        worker.CreatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        await _workersRepository.CreateAsync(worker);
        await _workersRepository.SaveChanges();

        return _mappingService.Map<WorkerVM, Worker>(worker);
    }

    public async Task Delete(Guid id)
    {
        var worker = await _workersRepository.GetById(id);

        if (worker is null)
            throw new UzWorksException($"Could not find worker with id : {id}");

        if (!_environmentAccessor.GetUserId().Equals(worker.Id))
            throw new UzWorksException("You have not access for delete this Worker.");

        _workersRepository.Delete(worker);
        await _workersRepository.SaveChanges();
    }

    public async Task<IEnumerable<WorkerVM>> GetAllAsync(
        int pageNumber, int pageSize, 
        Guid? jobCategoryId, int? maxAge, 
        int? minAge, uint? maxSalary, 
        uint? minSalary, string? gender, 
        Guid? regionId, Guid? districtId)
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

    public Task<int> GetCount()
    {
        return _workersRepository.GetWorkersCount();
    }

    public async Task<IEnumerable<WorkerVM>> GetWorkersByUserId(Guid userId)
    {
        if (userId.ToString() == null)
            throw new UzWorksException("Could not be null userId for get Your Jobs");

        if (!_environmentAccessor.IsAuthorOrSupervisor(userId))
            throw new UzWorksException($"You have not access to get this {userId}'s jobs.");

        var workers = await _workersRepository.GetWorkersByUserIdAsync(userId);
        var result = _mappingService.Map<IEnumerable<WorkerVM>,IEnumerable<Worker>>(workers);
        return result;
    }

    public async Task<WorkerVM> Update(WorkerEM workerEM)
    {
        if (workerEM is null)
            throw new UzWorksException("Could not be null worker edit model.");

        var worker = _mappingService.Map<Worker, WorkerEM>(workerEM);

        if (!_environmentAccessor.IsAuthorOrSupervisor(worker.CreatedBy))
            throw new UzWorksException("You have not access for update this worker model.");

        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();
        return _mappingService.Map<WorkerVM, Worker>(worker);
    }
}
