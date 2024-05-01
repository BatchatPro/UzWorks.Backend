using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Contacts;

public class ContactEM
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; }

    [Required]
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Message { get; set; }

    [Required]
    public bool IsComplated { get; set; }
}
