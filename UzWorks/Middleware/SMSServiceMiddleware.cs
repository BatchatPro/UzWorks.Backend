using System.Text;

namespace UzWorks.API.Middleware;

public static class SMSServiceMiddleware
{
    public static async Task LoginToEskizSMSService(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            // Log request headers
            Console.WriteLine("Request Headers:");
            foreach (var header in context.Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

            // Log request body
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine($"Request Body: {requestBody}");
                context.Request.Body.Position = 0; // Reset the request body stream position
            }

            await next();
        });

        var configuration = app.ApplicationServices.GetService(typeof(IConfiguration)) as IConfiguration;
        var email = configuration.GetValue<string>("SMSService:Email");
        var password = configuration.GetValue<string>("SMSService:Password");

        var client = new HttpClient();
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("email", email),
            new KeyValuePair<string, string>("password", password),
        });

        var response = await client.PostAsync("https://notify.eskiz.uz/api/auth/login", formContent);
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content); // You might want to handle the response content appropriately
    }
}
