using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Jobs;

public class JobsRepository : GenericRepository<Job>, IJobsRepository 
{
    public JobsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<Job[]> GetAllJobsAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, Guid? regionId, Guid? districtId)
    {
        var query = _context.Jobs.AsQueryable();

        if (jobCategoryId is not null)
            query = query.Where(x => x.CategoryId.Equals(jobCategoryId));

        if (maxAge is not null)
            query = query.Where(x => x.MaxAge < maxAge);

        if (minAge is not null)
            query = query.Where(x => x.MinAge > minAge);

        if (maxSalary is not null)
            query = query.Where(x => (x.Salary < maxSalary));

        if (minSalary is not null)
            query = query.Where(x => (x.Salary > minSalary));

        if (string.IsNullOrEmpty(gender))
            query = query.Where(x => x.Gender.Equals(gender));

        if (regionId is not null)
            query = query.Where(x => x.District.RegionId.Equals(regionId));

        if (districtId is not null)
            query = query.Where(x => x.DistrictId.Equals(districtId));

        if (pageNumber != 0 && pageSize != 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        return await query.ToArrayAsync();
    }

    public async Task<int> GetJobsCount()
    {
        return await _context.Jobs.CountAsync();
    }
}
