using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UzWorks.Core.DataTransferObjects.Auth;

namespace UzWorks.Web.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IActionResult> ForgetPassword(ForgetPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<LoginResponseDto>> LoginAsync(LoginDto dto)
    {
        var jsonRequest = JsonConvert.SerializeObject(dto);
        var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("", stringContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<>(jsonResponse);
        return jsonResponse;
    }

    public async Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> ResetPassword(ResetPasswordByCodeDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<SignUpResponseDto>> SignUpAsync(SignUpDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberDto dto)
    {
        throw new NotImplementedException();
    }
}
