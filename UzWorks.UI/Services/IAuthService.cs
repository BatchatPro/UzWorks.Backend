using UzWorks.UI.Models;

namespace UzWorks.UI.Services;

public interface IAuthService
{
    Task<string> LoginAsync(LoginRequestModel loginRequestModel);
}
