using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Workers.Experiences;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Experiences;

namespace UzWorks.API.Controllers;

public class ExperienceController : BaseController
{
    private readonly IExperienceService _experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpPost]
    public async Task<ActionResult<ExperienceVM>> Create([FromBody] ExperienceDto experienceDto)
    {
        var result = await _experienceService.Create(experienceDto);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperienceVM>>> GetAll()
    {
        var result = await _experienceService.GetAllExperiences();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ExperienceVM>> GetById([FromRoute] Guid id)
    {
        var result = await _experienceService.GetById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ExperienceVM>>> GetByUserId([FromRoute] Guid id)
    {
        var result = await _experienceService.GetExperiencesByUserId(id);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpPut]
    public async Task<ActionResult<ExperienceVM>> Update([FromBody] ExperienceEM experienceEM)
    {
        var result = await _experienceService.Update(experienceEM);
        return Ok(result);
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _experienceService.Delete(id);
        return Ok();
    }
}
