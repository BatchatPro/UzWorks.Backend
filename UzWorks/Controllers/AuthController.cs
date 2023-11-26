using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UzWorks.Core.AccessConfigurations;
using UzWorks.Core.DataTransferObjects.Auth;
using UzWorks.Identity.Models;
using UzWorks.Core.Constants;

namespace UzWorks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IOptions<AccessConfiguration> _siteSettings;
        private readonly UserManager<User> _userManager;

        public AuthController(IOptions<AccessConfiguration> siteSettings, UserManager<User> userManager)
        {
            _siteSettings = siteSettings;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Model state is not valid.");

            User user = await _userManager.FindByNameAsync(loginDto.UserName);
            
            if (user != null &&  await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey.TheSecretKey));

                AddRolesToClaims(authClaims, roles);
                var token = new JwtSecurityToken(
                    issuer: _siteSettings.Value.Issuer,
                    audience: _siteSettings.Value.Audience,
                    expires: DateTime.Now.AddDays(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = user.Id,
                    email = user.Email,
                    firstname = user.FirstName,
                    lastName = user.LastName,
                    gender = user.Gender,
                    phoneNumber = user.PhoneNumber,
                    birthDate = user.BirthDate,
                    access = roles,
                });
            }

            return NotFound();
        }

        private void AddRolesToClaims (List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }


        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state isn't valid.");

            if (signUpDto.Role != RoleNames.Employer && signUpDto.Role != RoleNames.Employee)
                return BadRequest($"Wrong Role! You have to select '{RoleNames.Employee}' or '{RoleNames.Employer}'.");

            User user = await _userManager.FindByNameAsync(signUpDto.UserName);

            if (user != null)
                return BadRequest("This user already created.");

            User newUser = new User(signUpDto.FirstName, signUpDto.LastName, signUpDto.UserName);

            var result = await _userManager.CreateAsync(newUser, signUpDto.Password);

            if (!result.Succeeded)
                return BadRequest("Didn't Succeeded.");
            
            await _userManager.AddToRolesAsync(newUser, new string[] { RoleNames.NewUser, signUpDto.Role });
            
            return Ok(result);

        }

    }
}
