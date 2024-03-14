using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Workers;

public class WorkersRepository : GenericRepository<Worker>, IWorkersRepository 
{
    public WorkersRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<Worker[]> GetAllWorkersAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, bool? status, Guid? regionId, Guid? districtId)
    {
        var query = _dbSet.AsQueryable();

        if (status != null)
        {
            query = query.Where(x => x.Status == true);
            query = query.Where(x => x.Deadline >= DateTime.Now);
        }

        if (jobCategoryId is not null)
            query = query.Where(x => x.CategoryId == jobCategoryId);

        if (maxAge is not null)
            query = query.Where(x => (DateTime.Now.Year - x.BirthDate.Year) < maxAge);

        if (minAge is not null)
            query = query.Where(x => (DateTime.Now.Year - x.BirthDate.Year) > minAge);

        if (maxSalary is not null)
            query = query.Where(x => (x.Salary < maxSalary));

        if (minSalary is not null)
            query = query.Where(x => (x.Salary > minSalary));

        if (!string.IsNullOrEmpty(gender))
            query = query.Where(x => x.Gender.Equals(gender));

        if (regionId is not null)
            query = query.Where(x => x.District.RegionId.Equals(regionId));

        if (districtId is not null)
            query = query.Where(x => x.DistrictId.Equals(districtId));

        if (pageNumber != 0 && pageSize != 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        return await query.ToArrayAsync();
    }

    public async Task<int> GetWorkersCount(bool? statusType) 
    {
        var query = _dbSet.AsQueryable();

        if (statusType == null || statusType == false)
            return await query.CountAsync();
    
        query = query.Where(x => x.Status == true);
        query = query.Where(x => x.Deadline >= DateTime.Now);
        
        return await query.CountAsync();
    }

    public async Task<int> GetWorkersCountForFilter(Guid? jobCategoryId = null, int? maxAge = null, int? minAge = null, uint? maxSalary = null, uint? minSalary = null, string? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null)
    {
        var query = _dbSet.AsQueryable();

        if (status != null)
        {
            query = query.Where(x => x.Status == true);
            query = query.Where(x => x.Deadline >= DateTime.Now);
        }

        if (jobCategoryId is not null)
            query = query.Where(x => x.CategoryId == jobCategoryId);

        if (maxAge is not null)
            query = query.Where(x => (DateTime.Now.Year - x.BirthDate.Year) < maxAge);

        if (minAge is not null)
            query = query.Where(x => (DateTime.Now.Year - x.BirthDate.Year) > minAge);

        if (maxSalary is not null)
            query = query.Where(x => (x.Salary < maxSalary));

        if (minSalary is not null)
            query = query.Where(x => (x.Salary > minSalary));

        if (!string.IsNullOrEmpty(gender))
            query = query.Where(x => x.Gender.Equals(gender));

        if (regionId is not null)
            query = query.Where(x => x.District.RegionId.Equals(regionId));

        if (districtId is not null)
            query = query.Where(x => x.DistrictId.Equals(districtId));

        return await query.CountAsync();
    }

    public async Task<Worker[]> GetWorkersByUserIdAsync(Guid userId)
    {
        var query = _dbSet.Where(x => (x.CreatedBy == userId));

        return await query.ToArrayAsync() ;
    }
    
    public async Task<Worker[]> GetTopWorkersAsync()
    {
        var query = _dbSet.Where(j => !j.IsDeleted).AsQueryable();
        query = query.Where(x => x.IsTop == true);
        query = query.Where(x => x.Status == true);
        query = query.Where(x => x.Deadline >= DateTime.Now);

        return await query.ToArrayAsync();
    }

}
