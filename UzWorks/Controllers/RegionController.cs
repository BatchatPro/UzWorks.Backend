using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Locations.Regions;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Location.Regions;

namespace UzWorks.API.Controllers;

public class RegionController : BaseController
{
    private readonly IRegionsService _regionsService;

    public RegionController(IRegionsService regionsService)
    {
        _regionsService = regionsService;
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPost]
    public async Task<ActionResult<RegionVM>> Create(RegionDto regionDto)
    {
        var result = await _regionsService.Create(regionDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _regionsService.Delete(id);
        return Ok();
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<ActionResult<RegionVM>> Edit([FromBody]RegionEM regionEM)
    {
        var result = await _regionsService.Update(regionEM);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<RegionVM>> GetById([FromRoute]Guid id)
    {
        var result = await _regionsService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegionVM>>> GetAll()
    {
        var result = await _regionsService.GetAllAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<RegionVM>> GetRegionByDistrictId([FromRoute]Guid id)
    {
        var result = await _regionsService.GetRegionByDistrictId(id);
        return Ok(result);
    }
}
