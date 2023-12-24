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
    public async Task<IActionResult> Create(RegionDto regionDto)
    {
        var result = await _regionsService.Create(regionDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _regionsService.Delete(id);
        return Ok();
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody]RegionEM regionEM)
    {
        var result = await _regionsService.Update(regionEM);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var result = await _regionsService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _regionsService.GetAllAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByRegionByDistrictId([FromRoute]Guid id)
    {
        var result = await _regionsService.GetByRegionByDistrictId(id);
        return Ok(result);
    }
}
