using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UzWorks.Core.AccessConfigurations;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Auth;
using UzWorks.Core.Exceptions;
using UzWorks.Identity.Constants;
using UzWorks.Identity.Models;
using UzWorks.Identity.SMS;

namespace UzWorks.Identity.Services.Auth;

public class AuthService : IAuthService
{
    private IOptions<AccessConfiguration> _siteSettings;
    private readonly UserManager<User> _userManager;
    private readonly UzWorksIdentityDbContext _context;
    private readonly ISmsSender _smsSender;

    public AuthService(IOptions<AccessConfiguration> siteSettings, UserManager<User> userManager, UzWorksIdentityDbContext context, ISmsSender smsSender)
    {
        _siteSettings = siteSettings;
        _userManager = userManager;
        _context = context;
        _smsSender = smsSender;
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.PhoneNumber) ??
                throw new UzWorksException("Not Found");

        if (!user.PhoneNumberConfirmed)
            throw new UzWorksException($"Please verify your phone number. {user.PhoneNumber}");

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new UzWorksException("Your Password is incorrect.");

        var roles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimNames.UserId, Convert.ToString(user.Id)),
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
                        user.FirstName, user.LastName, user.Gender,
                        user.BirthDate, user.PhoneNumber, roles);
    }

    public async Task<SignUpResponseDto> Register(SignUpDto signUpDto)
    {
        if (signUpDto.Role is not (RoleNames.Employer or RoleNames.Employee))
            throw new UzWorksException($"Please select '{RoleNames.Employee}' or '{RoleNames.Employer}' as your role.");

        User user = await _userManager.FindByNameAsync(signUpDto.PhoneNumber);

        if (user != null)
            throw new UzWorksException("This user already created.");

        User newUser = new User(signUpDto.FirstName, signUpDto.LastName, signUpDto.PhoneNumber);

        var result = await _userManager.CreateAsync(newUser, signUpDto.Password);

        if (!result.Succeeded)
            throw new UzWorksException("Didn't Succeeded.");

        var roles = new List<string> { RoleNames.NewUser, signUpDto.Role };

        await _userManager.AddToRolesAsync(newUser, roles);

        var smsResponce = await _smsSender.SendSmsAsync(signUpDto.PhoneNumber);

        if (smsResponce.IsSuccessStatusCode)
            throw new UzWorksException("SMS not sent.");

        return new SignUpResponseDto(Guid.Parse(newUser.Id), newUser.PhoneNumber, newUser.FirstName, newUser.LastName, roles);
    }

    public async Task<bool> VerifyPhoneNumber(string phoneNumber, string user_code)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber) ??
            throw new UzWorksException("User not found.");

        var code = _context.SmsTokens.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault().SmsCode ??
            throw new UzWorksException("Code not found.");

        if (code != user_code)
            throw new UzWorksException("Code is incorrect.");

        user.PhoneNumberConfirmed = true;
        await _userManager.UpdateAsync(user);

        return true;
    }

    public async Task<bool> ForgetPassword(string phoneNumber)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber) ??
            throw new UzWorksException("User not found.");

        var smsResponce = await _smsSender.SendSmsAsync(user.PhoneNumber);

        if (smsResponce.IsSuccessStatusCode)
            throw new UzWorksException("SMS not sent.");

        return true;
    }

    public async Task<bool> ResetPassword(string phoneNumber, string newPassword, string code)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber) ??
            throw new UzWorksException("User not found.");

        var smsToken = _context.SmsTokens.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault() ??
            throw new UzWorksException("Code not found.");

        if (smsToken.SmsCode != code)
            throw new UzWorksException("Code is incorrect.");

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, newPassword);

        return true;
    }
}
