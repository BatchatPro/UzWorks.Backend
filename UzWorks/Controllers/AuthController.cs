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
        public async Task<ActionResult<SignUpResponseDto>> SignUpAsync([FromBody] SignUpDto signUpDto)
        {
            return Ok(await _authService.Register(signUpDto));  
        }
    }
}
