using Microsoft.AspNetCore.Identity;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Identity.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderEnum? Gender { get; set; } = Core.Enums.GenderTypes.GenderEnum.Unknown;
    public DateTime BirthDate { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string? MobileId { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
    public Guid? DistrictId { get; set; }
    public string? DistrcitName { get; set; }
    public string? RegionName {  get; set; } 

    public User(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = phoneNumber;
        PhoneNumber = phoneNumber;
    }

    public User(string firstName, string lastName, string phoneNumber, string email, GenderEnum gender, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName= phoneNumber;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
        BirthDate = birthDate;
    }
}
