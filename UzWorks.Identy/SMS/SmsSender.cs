using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using UzWorks.Core.Entities.SMS;
using UzWorks.Core.Exceptions;

namespace UzWorks.Identity.SMS;

public class SmsSender : ISmsSender
{
    private readonly SmsClientOptions _options;
    private readonly UzWorksIdentityDbContext _context;
    private readonly EskizTokenHandler _eskizTokenHandler;

    public SmsSender(IOptions<SmsClientOptions> options, UzWorksIdentityDbContext context, EskizTokenHandler eskizTokenHandler)
    {
        _options = options.Value;
        _context = context;
        _eskizTokenHandler = eskizTokenHandler;
    }


    public async Task SendSmsOtpAsync(string phoneNumber)
    {
        var code = new Random().Next(1000, 9999).ToString();
        var smsToken = await _context.SmsTokens.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        if (smsToken != null)
            _context.SmsTokens.Remove(smsToken);

        await _context.SmsTokens.AddAsync(new SmsToken(code, phoneNumber));
        await _context.SaveChangesAsync();

        var verfication_message = $"Assalomu aleykum. UzWorks platformasi uchun tasdiqlash kodi: {code}";
        
        var response = await SendSmsAsync(phoneNumber, verfication_message);

        if (!response.IsSuccessStatusCode)
        {
            try{
                await _eskizTokenHandler.RefreshToken();
                await SendSmsAsync(phoneNumber, verfication_message);
            }
            catch(Exception ex)
            {
                throw new UzWorksException("Failed to send sms", ex);
            }
        }   
    }

    private async Task<HttpResponseMessage> SendSmsAsync(string phoneNumber, string message)
    {
        var token = await _eskizTokenHandler.GetToken();
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var stringContent = JsonConvert.SerializeObject(new
        {
            mobile_phone = phoneNumber,
            message,
            from = _options.Sender
        });

        var content = new StringContent(stringContent, Encoding.UTF8, "application/json");

        return await client.PostAsync(_options.UrlForSendSms, content);
    }
}
