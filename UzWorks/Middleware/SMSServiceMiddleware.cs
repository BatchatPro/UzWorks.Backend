namespace UzWorks.API.Middleware;

public static class SMSServiceMiddleware
{
    public async static Task LoginToEskizSMSService()
    {
        var email = "goblindev02@email.uz";
        var password = "200217iyunA@";

        var client = new HttpClient();
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("email", email),
            new KeyValuePair<string, string>("password", password),
        });

        var response = await client.PostAsync("https://notify.eskiz.uz/api/auth/login", formContent);
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content);
    }

}
