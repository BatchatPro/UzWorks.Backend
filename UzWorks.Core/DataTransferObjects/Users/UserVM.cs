using System.ComponentModel.DataAnnotations;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Core.DataTransferObjects.Users;

public class UserVM
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }
    public string? MobileId { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid? DistrictId { get; set; }
    public string? DistrictName { get; set; }
    public string? RegionName { get; set; }
}
