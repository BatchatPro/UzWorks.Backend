using UzWorks.Core.DataTransferObjects.Auth;

namespace UzWorks.Identity.Services.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginDto loginDto);
    Task<SignUpResponseDto> Register(SignUpDto registerDto);
}
