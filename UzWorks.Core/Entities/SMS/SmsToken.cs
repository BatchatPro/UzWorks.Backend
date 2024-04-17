namespace UzWorks.Core.Entities.SMS;

public class SmsToken : BaseEntity
{
    public string SmsCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
