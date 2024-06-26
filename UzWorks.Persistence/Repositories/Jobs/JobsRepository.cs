﻿using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Enums.GenderTypes;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.Jobs;

public class JobsRepository : GenericRepository<Job>, IJobsRepository
{
    public JobsRepository(UzWorksDbContext context) : base(context)
    {
    }

    public async Task<Job[]> GetAllAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, int? gender, bool? status, Guid? regionId, Guid? districtId)
    {
        var query = _dbSet.Where(j => !j.IsDeleted).AsQueryable();


        if (status != null) { 
            query = query.Where(x => x.Status == true);
            query = query.Where(x => x.Deadline >= DateTime.Now);
        }

        if (jobCategoryId != null)
            query = query.Where(x => x.CategoryId == jobCategoryId);

        if (maxAge != null)
            query = query.Where(x => x.MaxAge <= maxAge);

        if (minAge != null)
            query = query.Where(x => x.MinAge >= minAge);

        if (maxSalary != null)
            query = query.Where(x => (x.Salary <= maxSalary));

        if (minSalary != null)
            query = query.Where(x => (x.Salary >= minSalary));

        if (gender is not null)
            query = query.Where(x => x.Gender.Equals((GenderEnum)gender));

        if (regionId != null)
            query = query.Where(x => x.District.RegionId.Equals(regionId));

        if (districtId != null)
            query = query.Where(x => x.DistrictId.Equals(districtId));

        if (pageNumber != 0 && pageSize != 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        return await query.ToArrayAsync();
    }

    public async Task<int> GetCount(bool? statusType)
    {
        var query = _dbSet.AsQueryable();

        if (statusType == null || statusType == false)
            return await _context.Jobs.CountAsync();
    
        query = query.Where(x => x.Status == true);
        query = query.Where(x => x.Deadline >= DateTime.Now);
        
        return await query.CountAsync();
    }

    public async Task<int> GetcountForFilter(Guid? jobCategoryId = null, int? maxAge = null, 
                            int? minAge = null, uint? maxSalary = null, uint? minSalary = null, 
                            int? gender = null, bool? status = null, Guid? regionId = null, Guid? districtId = null)
    {
        var query = _dbSet.Where(j => !j.IsDeleted).AsQueryable();


        if (status != null)
        {
            query = query.Where(x => x.Status == true);
            query = query.Where(x => x.Deadline >= DateTime.Now);
        }

        if (jobCategoryId != null)
            query = query.Where(x => x.CategoryId == jobCategoryId);

        if (maxAge != null)
            query = query.Where(x => x.MaxAge <= maxAge);

        if (minAge != null)
            query = query.Where(x => x.MinAge >= minAge);

        if (maxSalary != null)
            query = query.Where(x => (x.Salary <= maxSalary));

        if (minSalary != null)
            query = query.Where(x => (x.Salary >= minSalary));

        if (gender is not null)
            query = query.Where(x => x.Gender.Equals((GenderEnum)gender));

        if (regionId != null)
            query = query.Where(x => x.District.RegionId.Equals(regionId));

        if (districtId != null)
            query = query.Where(x => x.DistrictId.Equals(districtId));

        return await query.CountAsync();
    }

    public async Task<Job[]> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(x => x.CreatedBy == userId).ToArrayAsync();
    }

    public async Task<Job[]> GetTopsAsync()
    {
        var query = _dbSet.Where(j => !j.IsDeleted).AsQueryable();
        query = query.Where(x => x.IsTop == true);
        query = query.Where(x => x.Status == true);
        query = query.Where(x => x.Deadline >= DateTime.Now);

        return await query.ToArrayAsync();
    }
}
