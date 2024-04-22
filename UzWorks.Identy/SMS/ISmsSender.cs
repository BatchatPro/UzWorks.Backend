namespace UzWorks.Identity.SMS;

public interface ISmsSender
{
    Task LogInToEskiz();
    Task RefreshToken();
    Task<HttpResponseMessage> SendSmsAsync(string phoneNumber);
}
