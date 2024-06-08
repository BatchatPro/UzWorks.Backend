using System.ComponentModel.DataAnnotations;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Core.DataTransferObjects.Users;

public class UserDto
{
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string RoleName { get; set; }
    
    [Required]
    public string Password { get; set; }

    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }
    public string? MobileId { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid? DistrictId { get; set; }
    public string? DistrictName { get; set; }
    public string? RegionName { get; set; }
}
