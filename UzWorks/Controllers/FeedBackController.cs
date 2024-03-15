using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.FeedBacks;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.FeedBacks;
using UzWorks.Core.Exceptions;

namespace UzWorks.API.Controllers;

public class FeedBackController : BaseController
{
    private readonly IFeedBackService _feedBackService;

    public FeedBackController(IFeedBackService feedBackService)
    {
        _feedBackService = feedBackService;
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPost]
    public async Task<ActionResult<FeedBackVM>> Create([FromBody] FeedBackDto dto)
    {
        try
        {
            var feedBack = await _feedBackService.Create(dto);
            return Ok(feedBack);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedBackVM>>> GetAll()
    {
        try
        {
            var feedBacks = await _feedBackService.GetAllAsync();
            return Ok(feedBacks);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut]
    public async Task<ActionResult<FeedBackVM>> Update([FromBody] FeedBackEM EM)
    {
        try
        {
            var feedBack = await _feedBackService.Update(EM);
            return Ok(feedBack);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> Delete(Guid Id)
    {
        try
        {
            var result = await _feedBackService.Delete(Id);
            return Ok(result);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
