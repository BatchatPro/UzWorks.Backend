using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UzWorks.BL.Services.Contacts;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Contacts;

namespace UzWorks.API.Controllers;

public class ContactController : BaseController
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<ContactVM>> Create([FromBody] ContactDto contactDto)
    {
        var contact = await _contactService.Create(contactDto);
        return Ok(contact);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<ActionResult<ContactVM>> GetAllContactsAsync(int pageNumber = 1, int pageSize = 15, bool? isComplated = null)
    {
        var contacts = await _contactService.GetAllContactsAsync(pageNumber, pageSize, isComplated);
        return Ok(contacts);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactVM>> GetById([FromRoute]Guid id)
    {
        var contact = await _contactService.GetById(id);
        return Ok(contact);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut]
    public async Task<ActionResult<ContactVM>> Update([FromBody] ContactEM contactEM)
    {
        var contact = await _contactService.Update(contactEM);
        return Ok(contact);
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPut("{id}/{status}")]
    public async Task<ActionResult<ContactVM>> ChangeStatus(Guid id, bool status)
    {
        var result = await _contactService.ChangeStatus(id, status);
        return result?Ok(_contactService.GetById(id)):BadRequest();
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(Guid id)
    {
        var result = await _contactService.Delete(id);
        return result?Ok("Delete saccessfull."):BadRequest();
    }
}
