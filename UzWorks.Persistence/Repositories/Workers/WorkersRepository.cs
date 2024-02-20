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
                        uint? minSalary, string? gender, Guid? regionId, Guid? districtId)
    {
        var query = _context.Workers.AsQueryable();

        query = query.Where(x => x.Status == true);

        if (jobCategoryId is not null)
            query = query.Where(x => x.CategoryId == jobCategoryId);
            //query = query.Where(x => x.CategoryId.Equals(jobCategoryId));

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

    public async Task<int> GetWorkersCount() 
    {
        return await _context.Workers.CountAsync();
    }

    public async Task<Worker[]> GetWorkersByUserIdAsync(Guid userId)
    {
        var query = _context.Workers.Where(x => (x.CreatedBy == userId));

        return await query.ToArrayAsync() ;
    }

}
