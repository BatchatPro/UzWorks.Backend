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

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpGet]
    public async Task<IActionResult> GetAllContactsAsync(int pageNumber = 1, int pageSize = 15, bool? isComplated = null)
    {
        var contacts = await _contactService.GetAllContactsAsync(pageNumber, pageSize, isComplated);
        return Ok(contacts);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var contact = await _contactService.GetById(id);
        return Ok(contact);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ContactDto contactDto)
    {
        var contact = await _contactService.Create(contactDto);
        return Ok(contact);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ContactEM contactEM)
    {
        var contact = await _contactService.Update(contactEM);
        return Ok(contact);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut("{id}/{status}")]
    public async Task<IActionResult> ChangeStatus(Guid id, bool status)
    {
        var result = await _contactService.ChangeStatus(id, status);
        return result?Ok(_contactService.GetById(id)):BadRequest();
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _contactService.Delete(id);
        return result?Ok("Delete saccessfull."):BadRequest();
    }
}
