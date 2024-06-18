using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.FAQs;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.FAQs;

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
        var faq = await _faqService.Create(dto);
        return Ok(faq);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FAQVM>>> GetAll()
    {
        var faqs = await _faqService.GetAllAsync();

        return Ok(faqs);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<FAQVM>> GetById(Guid id)
    {
        var faq = await _faqService.GetById(id);
        return Ok(faq);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut]
    public async Task<ActionResult<FAQVM>> Update([FromBody] FAQEM EM)
    {
        var faq = await _faqService.Update(EM);
        return Ok(faq);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        var result = await _faqService.Delete(id);
        return Ok(result);
    }
}
