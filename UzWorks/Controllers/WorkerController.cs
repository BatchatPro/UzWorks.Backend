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
    public async Task<ActionResult<WorkerVM>> Create([FromBody] WorkerDto workerDto)
    {
        var result = await _workerService.Create(workerDto);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkerVM>>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        var result = await _workerService.GetAllAsync(
                         pageNumber, pageSize, jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, true, regionId, districtId);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerVM>> GetById([FromRoute] Guid id)
    {
        var result = await _workerService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkerVM>>> GetTopWorkers()
    {
        var result = await _workerService.GetTops();
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkerVM>>> GetAllForAdmin([FromQuery] int pageNumber, [FromQuery] int pageSize,
                                            [FromQuery] Guid? jobCategoryId, [FromQuery] int? maxAge,
                                            [FromQuery] int? minAge, [FromQuery] uint? maxSalary,
                                            [FromQuery] uint? minSalary, [FromQuery] string? gender,
                                            [FromQuery] Guid? regionId, [FromQuery] Guid? districtId)
    {
        var result = await _workerService.GetAllAsync(
                         pageNumber, pageSize, jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, null, regionId, districtId);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{status}")]
    public async Task<ActionResult<int>> GetCount([FromRoute]bool? status)
    {
        var result = await _workerService.GetCount(status);
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
        var result = await _workerService.GetCountForFilter(jobCategoryId,
                         maxAge, minAge, maxSalary, minSalary,
                         gender, true, regionId, districtId);
        
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<WorkerVM>>> GetWorkersByUserId([FromRoute] Guid id)
    {
        var result = await _workerService.GetByUserId(id);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpPut]
    public async Task<ActionResult<WorkerVM>> Update([FromBody] WorkerEM workerEM)
    {
        var result = await _workerService.Update(workerEM);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut("{id}")]
    public async Task<ActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] bool status)
    {
        var result = await _workerService.ChangeStatus(id, status);
        
        if (result)
            return Ok();

        return BadRequest();
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _workerService.Delete(id);
        return Ok();
    }
}

