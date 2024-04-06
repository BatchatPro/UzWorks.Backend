using Newtonsoft.Json;
using System.Text;

namespace UzWorks.API.Middleware;

public static class SMSServiceMiddleware
{
    public static async Task LoginToEskizSMSService(this IApplicationBuilder app)
    {
        var configuration = app.ApplicationServices.GetService(typeof(IConfiguration)) as IConfiguration;
        var email = configuration.GetValue<string>("SMSService:Email");
        var password = configuration.GetValue<string>("SMSService:Password");

        var client = new HttpClient();
        
        var stringContent = JsonConvert.SerializeObject(new
        {
            email,
            password
        });

        var content = new StringContent(stringContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://notify.eskiz.uz/api/auth/login", content);
        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString); 
    }
}
