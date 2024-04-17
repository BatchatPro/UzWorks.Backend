using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UzWorks.Identity.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string? MobileId { get; set; } = string.Empty;

    [Required]
    [ProtectedPersonalData]
    public override string PhoneNumber { get; set; }

    public User(string firstName, string lastName, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        PhoneNumber = userName;
    }

    public User(string firstName, string lastName, string userName, string email, string? gender, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName= userName;
        Email = email;
        Gender = gender;
        BirthDate = birthDate;
        PhoneNumber = userName;
    }
}
