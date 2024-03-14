﻿using UzWorks.Core.DataTransferObjects.Jobs;

namespace UzWorks.Web.Services;

public interface IJobService
{
    Task<IList<JobVM>> GetAllJobs();
    Task<JobVM> GetJobById(Guid id);
}
