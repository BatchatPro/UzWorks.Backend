using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Jobs;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Districts;
using UzWorks.Persistence.Repositories.JobCategories;
using UzWorks.Persistence.Repositories.Jobs;
using UzWorks.Persistence.Repositories.Regions;

namespace UzWorks.BL.Services.Jobs;

public class JobService : IJobService
{
    private readonly IJobsRepository _jobsRepository;
    private readonly IMappingService _mappingService;
    private readonly IEnvironmentAccessor _environmentAccessor;
    private readonly IDistrictsRepository _districtsRepository;
    private readonly IRegionsRepository _regionsRepository;
    private readonly IJobCategoriesRepository _jobCategoriesRepository;

    public JobService(
                IJobsRepository jobsRepository, 
                IMappingService mappingService, 
                IEnvironmentAccessor environmentAccessor, 
                IDistrictsRepository districtsRepository,
                IRegionsRepository regionsRepository,
                IJobCategoriesRepository jobCategoriesRepository)
    {
        _jobsRepository = jobsRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor; 
        _districtsRepository = districtsRepository;
        _regionsRepository = regionsRepository;
        _jobCategoriesRepository = jobCategoriesRepository;
    }

    public async Task<JobVM> Create(JobDto jobDto)
    {
        if (jobDto == null)
            throw new UzWorksException("Job Dto can not be null.");

        if (!await _districtsRepository.IsExist(jobDto.DistrictId))
            throw new UzWorksException($"Could not find district with id: {jobDto.DistrictId}");

        if (!await _jobCategoriesRepository.IsExist(jobDto.CategoryId))
            throw new UzWorksException($"Could not find job category with id: {jobDto.CategoryId}");

        var job = _mappingService.Map<Job, JobDto>(jobDto);

        var userId = Guid.Parse(_environmentAccessor.GetUserId());
        var isAdmin = _environmentAccessor.IsAdmin(userId);

        job.CreateDate = DateTime.Now;
        job.CreatedBy = userId;

        if (isAdmin)
        {
            job.Status = true;
            job.IsTop = true;   
        }

        var district = await _districtsRepository.GetById(jobDto.DistrictId) ??
            throw new UzWorksException("District not found");

        var jobCategory = await _jobCategoriesRepository.GetById(jobDto.CategoryId);
        var region = await _regionsRepository.GetByDistrictId(district.Id);

        job.District = district;
        job.JobCategory = jobCategory;
        job.District.Region = region;

        await _jobsRepository.CreateAsync(job);
        await _jobsRepository.SaveChanges();

        return _mappingService.Map<JobVM, Job>(job);
    }

    public async Task<IEnumerable<JobVM>> GetAllAsync(int pageNumber, int pageSize, Guid? jobCategoryId, int? maxAge, 
                                                int? minAge, uint? maxSalary, uint? minSalary, string? gender, 
                                                bool? status, Guid? regionId, Guid? districtId)
    {
        var jobs = await _jobsRepository.GetAllAsync(
            pageNumber, pageSize, jobCategoryId, 
            maxAge, minAge, maxSalary, minSalary, 
            gender, status, regionId, districtId);

        var result = _mappingService.Map<IEnumerable<JobVM>, IEnumerable<Job>>(jobs);
        return result;
    }

    public async Task<JobVM> GetById(Guid id)
    {
        var job = await _jobsRepository.GetById(id) ??
            throw new UzWorksException($"Could not find job with id: {id}");

        return _mappingService.Map<JobVM, Job>(job);
    }

    public async Task<IEnumerable<JobVM>> GetTops() 
    { 
        var jobs = await _jobsRepository.GetTopsAsync();
        var result = _mappingService.Map<IEnumerable<JobVM>, IEnumerable<Job>>(jobs);
    
        return result;
    }

    public Task<int> GetCount(bool? statys)
    {
        return _jobsRepository.GetCount(statys);
    }

    public async Task<IEnumerable<JobVM>> GetByUserId(Guid userId)
    {
        if (!_environmentAccessor.IsAuthorOrSupervisor(userId))
            throw new UzWorksException($"You have not access to get this {userId}'s jobs.");

        var jobs = await _jobsRepository.GetByUserIdAsync(userId);
        var result = _mappingService.Map<IEnumerable<JobVM>, IEnumerable<Job>>(jobs);

        return result;
    }

    public async Task<int> GetGountForFilter(Guid? jobCategoryId, int? maxAge,
                                       int? minAge, uint? maxSalary, uint? minSalary, string? gender,
                                       bool? status, Guid? regionId, Guid? districtId)
    {
        return await _jobsRepository.GetcountForFilter(
                                        jobCategoryId,
                                        maxAge, minAge, maxSalary, minSalary,
                                        gender, status, regionId, districtId);
    }

    public async Task<JobVM> Update(JobEM jobEM)
    {
        var job = await _jobsRepository.GetById(jobEM.Id) ?? 
            throw new UzWorksException($"Could not find job with {jobEM.Id}");
        
        if (!await _districtsRepository.IsExist(jobEM.DistrictId))
            throw new UzWorksException($"Could not find district with id: {jobEM.DistrictId}");

        if (!await _jobCategoriesRepository.IsExist(jobEM.CategoryId))
            throw new UzWorksException($"Could not find job category with id: {jobEM.CategoryId}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(job.CreatedBy))
            throw new UzWorksException("You have not access to change this Job data.");

        _mappingService.Map(jobEM, job);

        var district = await _districtsRepository.GetById(jobEM.DistrictId)??
            throw new UzWorksException($"Could not find district with id: {jobEM.DistrictId}");

        var jobCategory = await _jobCategoriesRepository.GetById(jobEM.CategoryId);
        var region = await _regionsRepository.GetByDistrictId(district.Id);
        var userId = Guid.Parse(_environmentAccessor.GetUserId());

        job.UpdateDate = DateTime.Now;
        job.UpdatedBy = userId;
        job.District = district;
        job.JobCategory = jobCategory;
        job.District.Region = region;

        if (!_environmentAccessor.IsAdmin(userId))
            job.Status = false;

        _jobsRepository.UpdateAsync(job);
        await _jobsRepository.SaveChanges();

        return _mappingService.Map<JobVM,Job>(job);
    }
    
    public async Task<bool> ChangeStatus(Guid id, bool status)
    {
        var job = await _jobsRepository.GetById(id) ??
            throw new UzWorksException($"Could not find job with id: {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(job.CreatedBy))
            throw new UzWorksException("You have not access to change this Job status.");

        job.Status = status;

        _jobsRepository.UpdateAsync(job);
        await _jobsRepository.SaveChanges();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var job = await _jobsRepository.GetById(id) ??
            throw new UzWorksException($"Could not find job with id: {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(job.CreatedBy))
            throw new UzWorksException("You have not access to change this Job data.");

        _jobsRepository.Delete(job);
        await _jobsRepository.SaveChanges();

        return true;
    }
}
