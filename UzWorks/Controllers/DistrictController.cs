using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Locations.Districts;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Location.Districts;

namespace UzWorks.API.Controllers;

public class DistrictController : BaseController
{
    private readonly IDistrictService _districtService;
    public DistrictController(IDistrictService districtService)
    {
        _districtService = districtService;
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPost] 
    public async Task<ActionResult<DistrictVM>> Create([FromBody]DistrictDto district)
    {
        var result = await _districtService.Create(district);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<DistrictVM>> GetById([FromRoute]Guid id)
    {
        var result = await _districtService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DistrictVM>>> GetAll()
    {
        var result = await _districtService.GetAllAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<DistrictVM>>> GetByRegionId([FromRoute]Guid id)
    {
        var result = await _districtService.GetByRegionId(id);
        return Ok(result);
    }
    
    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<ActionResult<DistrictVM>> Update([FromBody]DistrictEM districtEM)
    {
        var result = await _districtService.Update(districtEM);
        return Ok(result);
    }

    
    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]Guid id)
    {
        var result = await _districtService.Delete(id);
        return result ? Ok() : BadRequest();
    }
}
