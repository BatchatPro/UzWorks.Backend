using Microsoft.AspNetCore.Identity;
using NullGuard;
namespace UzWorks.Identity.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [AllowNull]
    public string? Gender { get; set; }

    [AllowNull]
    public DateTime BirthDate { get; set; }

    [AllowNull]
    public string? MobileId { get; set; } = string.Empty;

    public User(string firstName, string lastName, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
    }

    public User(string firstName, string lastName, string userName, string email, string? gender, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName= userName;
        Email = email;
        Gender = gender;
        BirthDate = birthDate;
    }
}
