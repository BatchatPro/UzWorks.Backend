using Microsoft.AspNetCore.Mvc;
using UzWorks.Core.DataTransferObjects.Auth;
using UzWorks.Identity.Services.Auth;

namespace UzWorks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

        [HttpPost]
        [Route("verify-phone")]
        public async Task<IActionResult> VerifyPhoneNumber([FromBody] VerifyDto verifyDto)
        {
            if (await _authService.VerifyPhoneNumber(verifyDto.PhoneNumber, verifyDto.Code))
                return Ok("Successfull!");
            return BadRequest("Not Saccessfull!");
        }
    }
}
