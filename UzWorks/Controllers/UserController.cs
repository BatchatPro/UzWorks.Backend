using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UzWorks.Core.Abstract;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.DataTransferObjects.Users;
using UzWorks.Identity.Models;
using UzWorks.Identity.Services.Roles;

namespace UzWorks.API.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserDto user)
    {
        await _userService.Create(user);
        return Ok();
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery]int pageNumber, [FromQuery]int pageSize, 
        [FromQuery]string? gender, [FromQuery] string? email, 
        [FromQuery] string? phoneNumber)
    {
        var users = await _userService.GetAll(pageNumber, pageSize, gender, email, phoneNumber);
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserVM>> GetById([FromRoute]Guid id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }

    [Authorize]
    [HttpGet("{id}")]   
    public async Task <ActionResult<IEnumerable<string>>> GetRoles([FromRoute] Guid id)
    {
        var roles = await _userService.GetUserRoles(id);
        return Ok(roles);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UserVM>> Update([FromBody] UserEM profileEM)
    {
        if (profileEM == null)
            return BadRequest("User is null");

        var result = await _userService.Update(profileEM);
        return Ok(result);
    }

    [HttpPut]
    [Authorize] 
    public async Task<ActionResult<ResetPasswordDto>> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        if (resetPasswordDto == null)
            return BadRequest("User is null");

        if (await _userService.ResetPassword(resetPasswordDto))
            return Ok();

        return BadRequest();
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<ActionResult> AddRolesToUser([FromBody] UserRolesDto userRoles)
    {
        await _userService.AddRolesToUser(userRoles);
        return Ok();
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete]
    public async Task<ActionResult> DeleteRolesFromUser([FromBody] UserRolesDto userRoles)
    {
        await _userService.DeleteRolesFromUser(userRoles);
        return Ok();
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]Guid id)
    {
        if(await _userService.Delete(id))
            return Ok();

        return BadRequest();
    }
}
