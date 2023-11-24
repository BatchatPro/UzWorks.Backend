using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Locations.Districts;
using UzWorks.Core.DataTransferObjects.Location.Districts;

namespace UzWorks.API.Controllers;

public class DistrictController : BaseController
{
    private readonly IDistrictService _districtService;
    public DistrictController(IDistrictService districtService)
    {
        _districtService = districtService;
    }

    [HttpPost] 
    public async Task<IActionResult> Create([FromBody]DistrictDto district)
    {
        var result = await _districtService.Create(district);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        await _districtService.Delete(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody]DistrictEM districtEM)
    {
        var result = await _districtService.Update(districtEM);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromBody]Guid id)
    {
        var result = await _districtService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = _districtService.GetAllAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByRegionId([FromRoute]Guid regionId)
    {
        var result = await _districtService.GetDistrictByRegionId(regionId);
        return Ok(result);
    }
}
