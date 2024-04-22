namespace UzWorks.Core.DataTransferObjects.Auth;

public class ResetPasswordByCodeDto
{
    public string PhoneNumber { get; set; }
    public string NewPassword { get; set; }
    public string Code { get; set; }
}
