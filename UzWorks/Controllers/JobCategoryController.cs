﻿using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<JobCategoryVM>> Create(JobCategoryDto jobCategoryDto)
    {
        var result = await _service.Create(jobCategoryDto);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobCategoryVM>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<JobCategoryVM>> GetById([FromRoute] Guid id)
    {
        var result = await _service.GetById(id);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<ActionResult<JobCategoryVM>> Update([FromBody] JobCategoryEM jobCategoryEM)
    {
        var result = await _service.Update(jobCategoryEM);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}
