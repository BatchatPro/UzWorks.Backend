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
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly IEnvironmentAccessor _environmentAccessor;
    
    public UserController(UserManager<User> userManager,IUserService userService, IEnvironmentAccessor environmentAccessor)
    {
        _userManager = userManager;
        _userService = userService;
        _environmentAccessor = environmentAccessor;
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
    public async Task<ActionResult<UserVM>> GetUserById([FromRoute]Guid id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]Guid id)
    {
        await _userService.Delete(id);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UserVM>> UpdateAsync([FromBody] UserEM profileEM)
    {
        if (profileEM == null)
            return BadRequest("User is null");

        var result = await _userService.Update(profileEM);
        return Ok(result);
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
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserDto user)
    {
        await _userService.Create(user);
        return Ok();
    }
}
