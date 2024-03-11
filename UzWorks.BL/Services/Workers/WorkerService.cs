using UzWorks.BL.Services.Locations.Districts;
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
    private readonly IEnvironmentAccessor _environmentAccessor;
    private readonly IDistrictService _districtService;

    public WorkerService(IWorkersRepository workersRepository, IMappingService mappingService, IEnvironmentAccessor environmentAccessor, IDistrictService districtService)
    {
        _workersRepository = workersRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor;
        _districtService = districtService;
    }

    public async Task<WorkerVM> Create(WorkerDto workerDto)
    {
        if (workerDto == null)
            throw new UzWorksException("Work Dto can not be null.");

        if (!await _districtService.IsExist(workerDto.DistrictId))
            throw new UzWorksException($"Could not find district with id: {workerDto.DistrictId}");

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

        if (!_environmentAccessor.IsAuthorOrSupervisor(worker.CreatedBy))
            throw new UzWorksException("You have not access for delete this Worker.");

        _workersRepository.Delete(worker);
        await _workersRepository.SaveChanges();
    }

    public async Task<IEnumerable<WorkerVM>> GetAllAsync(
                        int pageNumber, int pageSize, 
                        Guid? jobCategoryId, int? maxAge, 
                        int? minAge, uint? maxSalary, 
                        uint? minSalary, string? gender, bool? status, 
                        Guid? regionId, Guid? districtId)
    {
        var workers = await _workersRepository.GetAllWorkersAsync(pageNumber, pageSize, jobCategoryId, 
                                                                maxAge, minAge, maxSalary, minSalary, 
                                                                gender, status, regionId, districtId);
        
        var result = _mappingService.Map<IEnumerable<WorkerVM>, IEnumerable<Worker>>(workers);
        return result;
    }

    public async Task<WorkerVM> GetById(Guid id)
    {
        var worker = await _workersRepository.GetById(id);
        
        if (worker is null) 
            throw new UzWorksException($"Could not find worker with id: {id}");

        var result = _mappingService.Map<WorkerVM, Worker>(worker);
        result.FullName = _environmentAccessor.GetFullName();

        return result; 
    }

    public Task<int> GetCount(bool? status)
    {
        return _workersRepository.GetWorkersCount(status);
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

        if (!await _districtService.IsExist(workerEM.DistrictId))
            throw new UzWorksException($"Could not find district with id: {workerEM.DistrictId}");

        var worker = _mappingService.Map<Worker, WorkerEM>(workerEM);

        if (!_environmentAccessor.IsAuthorOrSupervisor(worker.CreatedBy))
            throw new UzWorksException("You have not access for update this worker model.");

        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();

        var result = _mappingService.Map<WorkerVM, Worker>(worker);
        result.FullName = _environmentAccessor.GetFullName();
        
        return result;
    }

    public async Task<bool> ChangeStatus(Guid id, bool status)
    {
        var worker = await _workersRepository.GetById(id);

        if (worker is null)
            throw new UzWorksException($"Could not find worker with id: {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(worker.CreatedBy))
            throw new UzWorksException("You have not access for change this worker status.");

        worker.Status = status;

        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();

        return true;
    }
}
