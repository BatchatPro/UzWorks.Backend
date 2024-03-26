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
        try
        {
            var result = await _jobService.Create(jobDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        try
        {
            var result = await _jobService.GetAllAsync(
                             pageNumber, pageSize, jobCategoryId,
                             maxAge, minAge, maxSalary, minSalary,
                             gender, true, regionId, districtId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<JobVM>> GetById([FromRoute] Guid id)
    {
        try
        {
            var result = await _jobService.GetById(id); 
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetTopJobs()
    {
        try
        {
            var result = await _jobService.GetTopJobs();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetAllForAdmin([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        try
        {
            var result = await _jobService.GetAllAsync(
                             pageNumber, pageSize, jobCategoryId,
                             maxAge, minAge, maxSalary, minSalary,
                             gender, null, regionId, districtId);

           return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{status}")]
    public async Task<ActionResult<int>> GetCount([FromRoute]bool? status)
    {
        try
        {
            var result = await _jobService.GetCount(status);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<int>> GetCountForFilter(
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        try
        {
            var result = await _jobService.GetGountForFilter(jobCategoryId,
                             maxAge, minAge, maxSalary, minSalary,
                             gender, true, regionId, districtId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<JobVM>>> GetByUserId([FromRoute] Guid id)
    {
        try
        {
            var result = await _jobService.GetJobsByUserId(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Employer)]
    [HttpPut]
    public async Task<ActionResult<JobVM>> Update([FromBody] JobEM jobEM)
    {
        try
        {
            var result = await _jobService.Update(jobEM);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut("{id}")]
    public async Task<ActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] bool status)
    {
        try
        {
            var result = await _jobService.ChangeStatus(id, status);
    
            if (result)
                return Ok();
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // can you write me try-catch block for this method?
    [Authorize(Roles = RoleNames.Employer)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            return (await _jobService.Delete(id)) ? Ok() : BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
