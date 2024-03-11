using UzWorks.BL.Services.Locations.Districts;
using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.DataTransferObjects.Location.Districts;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Jobs;

namespace UzWorks.BL.Services.Jobs;

public class JobService : IJobService
{
    private readonly IJobsRepository _jobsRepository;
    private readonly IDistrictService _districtService;
    private readonly IMappingService _mappingService;
    private readonly IEnvironmentAccessor _environmentAccessor;
    public JobService(IJobsRepository jobsRepository, IMappingService mappingService, IEnvironmentAccessor environmentAccessor, IDistrictService districtService)
    {
        _jobsRepository = jobsRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor; 
        _districtService = districtService;
    }


    public async Task<JobVM> Create(JobDto jobDto)
    {
        if (jobDto == null)
            throw new UzWorksException("Job Dto can not be null.");

        if (!await _districtService.IsExist(jobDto.DistrictId))
            throw new UzWorksException($"Could not find district with id: {jobDto.DistrictId}");

        var job = _mappingService.Map<Job, JobDto>(jobDto);

        job.CreateDate = DateTime.Now;
        job.CreatedBy = Guid.Parse(_environmentAccessor.GetUserId());
        
        await _jobsRepository.CreateAsync(job);
        await _jobsRepository.SaveChanges();

        return _mappingService.Map<JobVM, Job>(job);
    }

    public async Task Delete(Guid id)
    {
        var job = await _jobsRepository.GetById(id);
        
        if (job == null)
            throw new UzWorksException($"Could not find job with id: {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(job.CreatedBy))
            throw new UzWorksException("You have not access to change this Job data.");

        _jobsRepository.Delete(job);
        await _jobsRepository.SaveChanges();
    }

    public async Task<IEnumerable<JobVM>> GetAllAsync(int pageNumber, int pageSize, Guid? jobCategoryId, int? maxAge, 
                                                int? minAge, uint? maxSalary, uint? minSalary, string? gender, 
                                                bool? status, Guid? regionId, Guid? districtId)
    {
        var jobs = await _jobsRepository.GetAllJobsAsync(
            pageNumber, pageSize, jobCategoryId, 
            maxAge, minAge, maxSalary, minSalary, 
            gender, status, regionId, districtId);

        var result = _mappingService.Map<IEnumerable<JobVM>, IEnumerable<Job>>(jobs);
        return result;
    }

    public async Task<JobVM> GetById(Guid id)
    {
        var job = await _jobsRepository.GetById(id);
        
        if( job is null)
            throw new UzWorksException($"Could not find job with id: {id}");

        return _mappingService.Map<JobVM, Job>(job);
    }

    public Task<int> GetCount(bool? statys)
    {
        return _jobsRepository.GetJobsCount(statys);
    }

    public async Task<IEnumerable<JobVM>> GetJobsByUserId(Guid userId)
    {
        if (userId.ToString() == null)
            throw new UzWorksException("Could not be null userId for get Your Jobs");

        if (!_environmentAccessor.IsAuthorOrSupervisor(userId))
            throw new UzWorksException($"You have not access to get this {userId}'s jobs.");

        var jobs = await _jobsRepository.GetJobsByUserIdAsync(userId);
        var result = _mappingService.Map<IEnumerable<JobVM>, IEnumerable<Job>>(jobs);
        return result;
    }

    public async Task<JobVM> Update(JobEM jobEM)
    {
        if (jobEM is null)
            throw new UzWorksException("Could not be null job edit model.");

        if (!await _districtService.IsExist(jobEM.DistrictId))
            throw new UzWorksException($"Could not find district with id: {jobEM.DistrictId}");

        var job = _mappingService.Map<Job, JobEM>(jobEM);

        if (!_environmentAccessor.IsAuthorOrSupervisor(job.CreatedBy))
            throw new UzWorksException("You have not access to change this Job data.");
        
        job.UpdateDate = DateTime.Now;
        job.UpdatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        _jobsRepository.UpdateAsync(job);
        await _jobsRepository.SaveChanges();

        return _mappingService.Map<JobVM,Job>(job);
    }
    
    public async Task<bool> ChangeStatus(Guid id, bool status)
    {
        var job = await _jobsRepository.GetById(id);

        if (job is null)
            throw new UzWorksException($"Could not find job with id: {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(job.CreatedBy))
            throw new UzWorksException("You have not access to change this Job status.");

        job.Status = status;
        _jobsRepository.UpdateAsync(job);
        await _jobsRepository.SaveChanges();
        return true;
    }
}
