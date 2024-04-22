using Microsoft.AspNetCore.Mvc;
using UzWorks.Core.DataTransferObjects.Auth;

namespace UzWorks.Web.Services.Auth;

public interface IAuthService
{
    Task<ActionResult<LoginResponseDto>> LoginAsync(LoginDto dto);
    Task<ActionResult<SignUpResponseDto>> SignUpAsync(SignUpDto dto);
    Task<IActionResult> VerifyPhoneNumber (VerifyPhoneNumberDto dto);
    Task<IActionResult> ResetPassword (ResetPasswordByCodeDto dto);
    Task<IActionResult> ForgetPassword (ForgetPasswordDto dto);

    Task LogoutAsync();
}
