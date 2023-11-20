﻿using UzWorks.Core.DataTransferObjects.Jobs;

namespace UzWorks.BL.Services.Jobs;

public interface IJobService
{
    Task<IEnumerable<JobVM>> GetAllAsync(int pageNumber, int pageSize,
                        Guid? jobCategoryId, int? maxAge, int? minAge, uint? maxSalary,
                        uint? minSalary, string? gender, Guid? regionId, Guid? districtId);
    Task<JobVM> GetById(Guid id);
    Task<JobVM> Create(JobDto jobDto);
    Task<JobVM> Update(JobEM jobEM);
    Task Delete(Guid id);
}
