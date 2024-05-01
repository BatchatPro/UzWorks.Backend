using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Auth;

public class ResetPasswordByCodeDto
{
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    public string PhoneNumber { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You have to enter new password.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "You have to enter verification code")]
    public string Code { get; set; }
}
