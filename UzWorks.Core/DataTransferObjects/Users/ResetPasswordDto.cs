namespace UzWorks.Core.DataTransferObjects.Users;

public class ResetPasswordDto
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string OldPassword { get; set; }
}
