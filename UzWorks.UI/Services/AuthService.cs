using Newtonsoft.Json;
using System.Text;
using UzWorks.UI.Models;

namespace UzWorks.UI.Services;

public class AuthService :  IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> LoginAsync(LoginRequestModel requestModel)
    {
        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://172.16.14.203:28/Auth/login", stringContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }
}
