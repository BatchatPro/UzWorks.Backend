using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UzWorks.Core.DataTransferObjects.Auth;
using UzWorks.Identity.Models;
using UzWorks.Core.Constants;
using UzWorks.Identity.Services.Auth;

namespace UzWorks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> LoginAsync([FromBody] LoginDto loginDto)
        {
            return Ok(await _authService.Login(loginDto));
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto signUpDto)
        {
            if (signUpDto.Role is not (RoleNames.Employer or RoleNames.Employee))
                return BadRequest($"Please select '{RoleNames.Employee}' or '{RoleNames.Employer}' as your role.");

            User user = await _userManager.FindByNameAsync(signUpDto.UserName);

            if (user != null)
                return BadRequest("This user already created.");

            User newUser = new User(signUpDto.FirstName, signUpDto.LastName, signUpDto.UserName);

            var result = await _userManager.CreateAsync(newUser, signUpDto.Password);

            if (!result.Succeeded)
                return BadRequest("Didn't Succeeded.");
            
            await _userManager.AddToRolesAsync(newUser, new[] { RoleNames.NewUser, signUpDto.Role });
            
            return Ok(result);
        }
    }
}
