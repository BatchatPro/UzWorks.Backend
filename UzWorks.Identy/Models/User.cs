using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Identity.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; } = Gender.Unknown;
    public DateTime BirthDate { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string? MobileId { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";

    [Required]
    [Phone]
    public override string PhoneNumber { get; set; }

    public User(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = phoneNumber;
        PhoneNumber = phoneNumber;
    }

    public User(string firstName, string lastName, string phoneNumber, string email, Gender gender, DateTime birthDate)
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
