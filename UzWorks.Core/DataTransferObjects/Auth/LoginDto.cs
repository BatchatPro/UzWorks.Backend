using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Auth;

public class LoginDto
{
    // Can you write me Required attribute with error message for each element
    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage ="Password is required.")]
    [MinLength(8)]
    [MaxLength(30)]
    public string Password { get; set; }
}
