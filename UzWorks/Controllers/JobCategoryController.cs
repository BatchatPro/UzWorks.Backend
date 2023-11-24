using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.JobCategories;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.JobCategories;

namespace UzWorks.API.Controllers;

public class JobCategoryController : BaseController
{
    private readonly IJobCategoryService _service;

    public JobCategoryController(IJobCategoryService service)
    {
        _service = service;
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPost]
    public async Task<IActionResult> Create(JobCategoryDto jobCategoryDto)
    {
        var result = await _service.Create(jobCategoryDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] JobCategoryEM jobCategoryEM)
    {
        var result = await _service.Update(jobCategoryEM);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _service.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

}
