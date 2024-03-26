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

    // can you write me try catch block for each method?
    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPost] 
    public async Task<ActionResult<DistrictVM>> Create([FromBody]DistrictDto district)
    {
        try
        {
            var result = await _districtService.Create(district);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<DistrictVM>> GetById([FromRoute]Guid id)
    {
        try
        {
            var result = await _districtService.GetById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DistrictVM>>> GetAll()
    {
        try
        {
            var result = await _districtService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<DistrictVM>>> GetByRegionId([FromRoute]Guid id)
    {
        try
        {
            var result = await _districtService.GetByRegionId(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<ActionResult<DistrictVM>> Update([FromBody]DistrictEM districtEM)
    {
        try
        {
            var result = await _districtService.Update(districtEM);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    
    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]Guid id)
    {
        try
        {
            var result = await _districtService.Delete(id);
            return result ? Ok() : BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
