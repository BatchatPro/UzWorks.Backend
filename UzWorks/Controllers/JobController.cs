using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Jobs;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Jobs;

namespace UzWorks.API.Controllers;

public class JobController : BaseController
{
    private readonly IJobService _jobService;
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpPost]
    public async Task<ActionResult<JobVM>> Create(JobDto jobDto)
    {
        var result = await _jobService.Create(jobDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _jobService.Delete(id);
        return Ok();
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpPut]
    public async Task<ActionResult<JobVM>> Edit([FromBody] JobEM jobEM)
    {
        var result = await _jobService.Update(jobEM);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut("{id}")]
    public async Task<ActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] bool status)
    {
        var result = await _jobService.ChangeStatus(id, status);

        if (result)
            return Ok();

        return BadRequest();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<JobVM>> GetById([FromRoute] Guid id)
    {
        var result = await _jobService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        var result = await _jobService.GetAllAsync(
                         pageNumber, pageSize, jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, true, regionId, districtId);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetAllForAdmin([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        var result = await _jobService.GetAllAsync(
                         pageNumber, pageSize, jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, null, regionId, districtId);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{status}")]
    public async Task<ActionResult<int>> GetCount([FromRoute]bool? status)
    {
        var result = await _jobService.GetCount(status);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<int>> GetCountForFilter(
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        var result = await _jobService.GetGountForFilter(jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, null, regionId, districtId);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetJobsByUserId([FromRoute] Guid id)
    {
        var result = await _jobService.GetJobsByUserId(id);
        return Ok(result);
    }
}
