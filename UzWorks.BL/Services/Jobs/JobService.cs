using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Jobs;

namespace UzWorks.BL.Services.Jobs;

public class JobService : IJobService
{
    private readonly IJobsRepository _jobsRepository;
    private readonly IMappingService _mappingService;

    public JobService(IJobsRepository jobsRepository, IMappingService mappingService)
    {
        _jobsRepository = jobsRepository;
        _mappingService = mappingService;
    }

    public async Task<JobVM> Create(JobDto jobDto)
    {
        if (jobDto == null)
            throw new UzWorksException("Job Dto can not be null.");

        var job = _mappingService.Map<Job, JobDto>(jobDto);
        await _jobsRepository.CreateAsync(job);
        await _jobsRepository.SaveChanges();

        return _mappingService.Map<JobVM, Job>(job);
    }

    public async Task Delete(Guid id)
    {
        var job = await _jobsRepository.GetById(id);
        
        if (job == null)
            throw new UzWorksException($"Could not find job with id: {id}");

        _jobsRepository.Delete(job);
        await _jobsRepository.SaveChanges();
    }

    public async Task<IEnumerable<JobVM>> GetAllAsync(int pageNumber, int pageSize, Guid? jobCategoryId, int? maxAge, 
                                                int? minAge, uint? maxSalary, uint? minSalary, string? gender, 
                                                Guid? regionId, Guid? districtId)
    {
        var jobs = await _jobsRepository.GetAllJobsAsync(
            pageNumber, pageSize, jobCategoryId, 
            maxAge, minAge, maxSalary, minSalary, 
            gender, regionId, districtId);

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

    public async Task<JobVM> Update(JobEM jobEM)
    {
        if (jobEM is null)
            throw new UzWorksException("Could not be null job edit model.");

        var job = _mappingService.Map<Job, JobEM>(jobEM);
        _jobsRepository.UpdateAsync(job);
        await _jobsRepository.SaveChanges();

        return _mappingService.Map<JobVM,Job>(job);
    }
}
