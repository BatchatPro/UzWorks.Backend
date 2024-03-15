using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.FAQs;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.FAQs;
using UzWorks.Core.Exceptions;

namespace UzWorks.API.Controllers;

public class FAQController : BaseController
{
    private readonly IFAQService _faqService;

    public FAQController(IFAQService faqService)
    {
        _faqService = faqService;
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPost]
    public async Task<ActionResult<FAQVM>> Create([FromBody] FAQDto dto)
    {
        try
        {
            var faq = await _faqService.Create(dto);
            return Ok(faq);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FAQVM>>> GetAll()
    {
        try
        {
            var faqs = await _faqService.GetAllAsync();
            return Ok(faqs);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut]
    public async Task<ActionResult<FAQVM>> Update([FromBody] FAQEM EM)
    {
        try
        {
            var faq = await _faqService.Update(EM);
            return Ok(faq);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        try
        {
            var result = await _faqService.Delete(id);
            return Ok(result);
        }
        catch (UzWorksException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
