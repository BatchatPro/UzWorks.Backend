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
        try
        {
            var result = await _experienceService.Create(experienceDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperienceVM>>> GetAll()
    {
        try
        {
            var result = await _experienceService.GetAllExperiences();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ExperienceVM>> GetById([FromRoute] Guid id)
    {
        try
        {
            var result = await _experienceService.GetById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ExperienceVM>>> GetByUserId([FromRoute] Guid id)
    {
        try
        {
            var result = await _experienceService.GetExperiencesByUserId(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpPut]
    public async Task<ActionResult<ExperienceVM>> Update([FromBody] ExperienceEM experienceEM)
    {
        try
        {
            var result = await _experienceService.Update(experienceEM);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Employee)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _experienceService.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
