using UzWorks.BL.Services.Locations.Districts;
using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Workers;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Identity.Services.Roles;
using UzWorks.Persistence.Repositories.Workers;

namespace UzWorks.BL.Services.Workers;

public class WorkerService : IWorkerService
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IMappingService _mappingService;
    private readonly IEnvironmentAccessor _environmentAccessor;
    private readonly IDistrictService _districtService;
    private readonly IUserService _userService;

    public WorkerService(
                    IWorkersRepository workersRepository, 
                    IMappingService mappingService, 
                    IEnvironmentAccessor environmentAccessor, 
                    IDistrictService districtService,
                    IUserService userService)
    {
        _workersRepository = workersRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor;
        _districtService = districtService;
        _userService = userService;
    }

    public async Task<WorkerVM> Create(WorkerDto workerDto)
    {
        if (!await _districtService.IsExist(workerDto.DistrictId))
            throw new UzWorksException($"Could not find district with id: {workerDto.DistrictId}");

        var worker = _mappingService.Map<Worker, WorkerDto>(workerDto) ??
            throw new UzWorksException("Could not map WorkerDto to Worker.");
        
        worker.CreateDate = DateTime.Now;
        worker.CreatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        if (_environmentAccessor.IsAdmin(Guid.Parse(_environmentAccessor.GetUserId())))
        {
            worker.Status = true;
            worker.IsTop = true;   
        }

        await _workersRepository.CreateAsync(worker);
        await _workersRepository.SaveChanges();

        var result = _mappingService.Map<WorkerVM, Worker>(worker);
        result.FullName = _environmentAccessor.GetFullName();

        return result;
    }

    public async Task<IEnumerable<WorkerVM>> GetAllAsync(
                        int pageNumber, int pageSize, 
                        Guid? jobCategoryId, int? maxAge, 
                        int? minAge, uint? maxSalary, 
                        uint? minSalary, string? gender, bool? status, 
                        Guid? regionId, Guid? districtId)
    {
        var workers = await _workersRepository.GetAllAsync(pageNumber, pageSize, jobCategoryId, 
                                                                maxAge, minAge, maxSalary, minSalary, 
                                                                gender, status, regionId, districtId);
        
        return _mappingService.Map<IEnumerable<WorkerVM>, IEnumerable<Worker>>(workers);
    }

    public async Task<WorkerVM> GetById(Guid id)
    {
        var worker = await _workersRepository.GetById(id) ??
            throw new UzWorksException($"Could not find worker with id: {id}");

        var result = _mappingService.Map<WorkerVM, Worker>(worker);

        result.FullName = await _userService.GetUserFullName(worker.CreatedBy ?? 
            throw new UzWorksException("Could not be null worker created by user id."));

        return result; 
    }

    public Task<int> GetCount(bool? status)
    {
        return _workersRepository.GetCount(status);
    }

    public async Task<IEnumerable<WorkerVM>> GetByUserId(Guid userId)
    {
        var workers = await _workersRepository.GetByUserIdAsync(userId);
        var result = _mappingService.Map<IEnumerable<WorkerVM>,IEnumerable<Worker>>(workers);
        
        return result;
    }

    public async Task<IEnumerable<WorkerVM>> GetTops()
    {
        var workers = await _workersRepository.GetTopsAsync();

        return _mappingService.Map<IEnumerable<WorkerVM>, IEnumerable<Worker>>(workers);
    }

    public Task<int> GetCountForFilter(Guid? jobCategoryId, int? maxAge,
                        int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, bool? status,
                        Guid? regionId, Guid? districtId)
    {
        return _workersRepository.GetCountForFilter(jobCategoryId,
                             maxAge, minAge, maxSalary, minSalary,
                             gender, status, regionId, districtId);
    }

    public async Task<WorkerVM> Update(WorkerEM workerEM)
    {
        var worker = await _workersRepository.GetById(workerEM.Id) ??
            throw new UzWorksException($"Could not find worker with id: {workerEM.Id}");
        
        if (!await _districtService.IsExist(workerEM.DistrictId))
            throw new UzWorksException($"Could not find district with id: {workerEM.DistrictId}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(worker.CreatedBy))
            throw new UzWorksException("You have not access for update this worker model.");

        _mappingService.Map(workerEM, worker);

        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();

        var result = _mappingService.Map<WorkerVM, Worker>(worker);

        result.FullName = await _userService.GetUserFullName(worker.CreatedBy ??
            throw new UzWorksException("Could not be null worker created by user id."));
        
        return result;
    }

    public async Task<bool> ChangeStatus(Guid id, bool status)
    {
        var worker = await _workersRepository.GetById(id) ??
            throw new UzWorksException($"Could not find worker with id: {id}");

        if (!_environmentAccessor.IsAdmin(worker.CreatedBy ?? 
                throw new UzWorksException("Could not be null worker created by user id.")))
            throw new UzWorksException("You have not access for change this worker status.");

        worker.Status = status;

        _workersRepository.UpdateAsync(worker);
        await _workersRepository.SaveChanges();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var worker = await _workersRepository.GetById(id) ??
            throw new UzWorksException($"Could not find worker with id : {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(worker.CreatedBy))
            throw new UzWorksException("You have not access for delete this Worker.");

        _workersRepository.Delete(worker);
        await _workersRepository.SaveChanges();

        return true;
    }
}
