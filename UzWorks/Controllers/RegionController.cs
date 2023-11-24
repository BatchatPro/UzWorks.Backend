using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Locations.Regions;
using UzWorks.Core.DataTransferObjects.Location.Regions;

namespace UzWorks.API.Controllers;

public class RegionController : BaseController
{
    private readonly IRegionsService _regionsService;

    public RegionController(IRegionsService regionsService)
    {
        _regionsService = regionsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegionDto regionDto)
    {
        var result = await _regionsService.Create(regionDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _regionsService.Delete(id);
        return Ok();
    }

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
}
