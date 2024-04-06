using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UzWorks.Core.AccessConfigurations;
using UzWorks.Core.DataTransferObjects.Auth;
using UzWorks.Core.Exceptions;
using UzWorks.Identity.Constants;
using UzWorks.Identity.Models;

namespace UzWorks.Identity.Services.Auth;

public class AuthService : IAuthService
{
    private IOptions<AccessConfiguration> _siteSettings;
    private readonly UserManager<User> _userManager;

    public AuthService(IOptions<AccessConfiguration> siteSettings, UserManager<User> userManager)
    {
        _siteSettings = siteSettings;
        _userManager = userManager;
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName) ??
                throw new UzWorksException("Not Found");

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new UzWorksException("Your Password is incorrect.");

        var roles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimNames.UserId, user.Id),
                    new Claim(ClaimNames.FirstName, user.FirstName),
                    new Claim(ClaimNames.LastName, user.LastName)
                };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey.TheSecretKey));

        // add roels to claims
        foreach (var role in roles)
        {
            var roleClaim = new Claim(ClaimTypes.Role, role);
            authClaims.Add(roleClaim);
        }

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _siteSettings.Value.Issuer,
            audience: _siteSettings.Value.Audience,
            expires: DateTime.Now.AddDays(10),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new LoginResponseDto(
                        Guid.Parse(user.Id), token, jwtSecurityToken.ValidTo,
                        user.UserName, user.FirstName, user.LastName, user.Gender,
                        user.BirthDate, user.PhoneNumber, roles);
    }

    public Task<SignUpResponseDto> Register(SignUpDto registerDto)
    {
        throw new NotImplementedException();
    }
}
