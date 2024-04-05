using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.FeedBacks;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.FeedBacks;

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
        var feedBack = await _feedBackService.Create(dto);
        return Ok(feedBack);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedBackVM>>> GetAll()
    {
        var feedBacks = await _feedBackService.GetAllAsync();
        return Ok(feedBacks);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut]
    public async Task<ActionResult<FeedBackVM>> Update([FromBody] FeedBackEM EM)
    {
        var feedBack = await _feedBackService.Update(EM);
        return Ok(feedBack);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> Delete(Guid Id)
    {
        var result = await _feedBackService.Delete(Id);
        return Ok(result);
    }
}
