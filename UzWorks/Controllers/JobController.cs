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
    public async Task<IActionResult> Create(JobDto jobDto)
    {
        var result = await _jobService.Create(jobDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _jobService.Delete(id);
        return Ok();
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] JobEM jobEM)
    {
        var result = await _jobService.Update(jobEM);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _jobService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        var result = await _jobService.GetAllAsync(
                         pageNumber, pageSize, jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, regionId, districtId);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCount()
    {
        var result = await _jobService.GetCount();
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobsByUserId([FromRoute] Guid id)
    {
        var result = await _jobService.GetJobsByUserId(id);
        return Ok(result);
    }
}