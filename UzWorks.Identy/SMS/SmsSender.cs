using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using UzWorks.Core.Entities.SMS;
using UzWorks.Core.Exceptions;

namespace UzWorks.Identity.SMS; 

public class SmsSender : ISmsSender
{
    private readonly SmsClientOptions _options;
    private readonly UzWorksIdentityDbContext _context;
    
    public SmsSender(IOptions<SmsClientOptions> options, UzWorksIdentityDbContext context)
    {
        _options = options.Value;
        _context = context;

        if (string.IsNullOrEmpty(_options.Token))
        {
            LogInToEskiz().Wait();
        }
    }

    public async Task LogInToEskiz()
    {
        var profile_email = _options.EskizEmail;
        var profile_password = _options.EskizPassword;

        using var httpClient = new HttpClient();

        var stringContent = JsonConvert.SerializeObject(new
        {
            email = profile_email,
            password = profile_password
        });

        var content = new StringContent(stringContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(_options.UrlForLoginToEskiz, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new UzWorksException("Failed to login to Eskiz.");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseData = JObject.Parse(responseContent);
        var token = responseData["data"]["token"].ToString();
        _options.Token = token;
    }

    public async Task RefreshToken()
    {
        var oldToken = _options.Token;
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {oldToken}");

        var response = await httpClient.PatchAsync(_options.UrlForRefreshEskizToken, null);

        if (!response.IsSuccessStatusCode)
        {
            throw new UzWorksException("Failed to refresh token.");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseData = JObject.Parse(responseContent);
        var newToken = responseData["data"]["token"].ToString();

        _options.Token = newToken;
    }

    public async Task<HttpResponseMessage> SendSmsAsync(string phoneNumber)
    {
        var code = new Random().Next(1000, 9999).ToString();
        var smsToken = _context.SmsTokens.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

        if(smsToken != null)
            _context.SmsTokens.Remove(smsToken);

        _context.SmsTokens.Add(new SmsToken(code, phoneNumber));
        _context.SaveChanges();

        var verfication_message = $"Assalomu aleykum. UzWorks platformasi uchun tasdiqlash kodi: {code}";

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_options.Token}");

        var stringContent = JsonConvert.SerializeObject(new
        {
            mobile_phone = phoneNumber,
            message = verfication_message,
            from = _options.Sender
        });
        
        var content = new StringContent(stringContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(_options.UrlForSendSms, content);

        if (HttpStatusCode.OK != response.StatusCode)
        {
            try{
                await RefreshToken();
            }
            catch(Exception ex)
            {
                throw new UzWorksException("Failed to send sms", ex);
            }
        }   

        return response;
    }
}
