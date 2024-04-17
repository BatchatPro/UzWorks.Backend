namespace UzWorks.Infrastructure.SMS;

public class SmsSender : ISmsSender
{
    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        var configuration = _configuration.GetSection("SmsOptions").Get<SmsOptions>();
        var client = new HttpClient();
        var stringContent = JsonConvert.SerializeObject(new
        {
            phoneNumber,
            message,
            sender = configuration.Sender
        });

        var content = new StringContent(stringContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(configuration.SMSApiUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }   
}
