using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Workers;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Workers;

namespace UzWorks.API.Controllers;

public class WorkerController : BaseController
{
    private readonly IWorkerService _workerService;

    public WorkerController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkerDto workerDto)
    {
        var result = await _workerService.Create(workerDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        await _workerService.Delete(id);
        return Ok();
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] WorkerEM workerEM)
    {
        var result = await _workerService.Update(workerEM);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _workerService.GetById(id);
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
        var result = await _workerService.GetAllAsync(
                         pageNumber, pageSize, jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, regionId, districtId);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpGet]
    public async Task<IActionResult> GetCount()
    {
        var result = await _workerService.GetCount();
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkersByUserId([FromRoute] Guid id)
    {
        var result = await _workerService.GetWorkersByUserId(id);
        return Ok(result);
    }
}
