using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Auth;

public class SignUpDto
{
    [Required(ErrorMessage = "This Poly is Required.")]
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "This Poly is Required.")]
    [StringLength(100, ErrorMessage = "Minimum Length = 8 !", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "This Poly is Required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "It is not the same Password!")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "This Poly is Required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "This Poly is Required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "This Poly is Required.")]
    public string Role { get; set; } = string.Empty;

}
