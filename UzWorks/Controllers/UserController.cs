using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UzWorks.Core.Abstract;
using UzWorks.Core.Constants;
using UzWorks.Identity.Services.Roles;

namespace UzWorks.API.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly IEnvironmentAccessor _environmentAccessor;
    
    public UserController(IUserService userService, IEnvironmentAccessor environmentAccessor)
    {
        _userService = userService;
        _environmentAccessor = environmentAccessor;
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery]int pageNumber, [FromQuery]int pageSize, 
        [FromQuery]string? gender, [FromQuery] string? email, 
        [FromQuery] string? phoneNumber)
    {
        var users = await _userService.GetAll(pageNumber, pageSize, gender, email, phoneNumber);
        return Ok(users);
    }

    [Authorize(Roles = RoleNames.SuperAdmin)]
    [HttpGet]
    public async Task<IActionResult> GeUserId()
    {
        var userId = _environmentAccessor.GetUserId();
        return Ok(userId);
    }

}
