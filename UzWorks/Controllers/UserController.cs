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
        try
        {
            await _userService.Create(user);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery]int pageNumber, [FromQuery]int pageSize, 
        [FromQuery]string? gender, [FromQuery] string? email, 
        [FromQuery] string? phoneNumber)
    {
        try 
        {
            var users = await _userService.GetAll(pageNumber, pageSize, gender, email, phoneNumber);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserVM>> GetById([FromRoute]Guid id)
    {
        try
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("{id}")]   
    public async Task <ActionResult<IEnumerable<string>>> GetRoles([FromRoute] Guid id)
    {
        try
        {
            var roles = await _userService.GetUserRoles(id);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UserVM>> Update([FromBody] UserEM profileEM)
    {
        try
        {
            var result = await _userService.Update(profileEM);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize] 
    public async Task<ActionResult<ResetPasswordDto>> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var result = await _userService.ResetPassword(resetPasswordDto);

            return result ? Ok() : BadRequest();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpPut]
    public async Task<ActionResult> AddRolesToUser([FromBody] UserRolesDto userRoles)
    {
        try
        {
            await _userService.AddRolesToUser(userRoles);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpDelete]
    public async Task<ActionResult> DeleteRolesFromUser([FromBody] UserRolesDto userRoles)
    {
        try
        {
            await _userService.DeleteRolesFromUser(userRoles);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = RoleNames.Supervisor)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute]Guid id)
    {
        try
        {
            var result = await _userService.Delete(id);
            return result ? Ok() : BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
