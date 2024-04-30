using System.ComponentModel.DataAnnotations;

namespace UzWorks.Core.DataTransferObjects.Contacts;

public class ContactEM
{
    public Guid Id { get; set; }
    public string Title { get; set; }   
    
    [RegularExpression("^998\\d{9}$", ErrorMessage = "Please enter a valid phone number starting with 998 and 12 digits long.")]
    public string PhoneNumber { get; set; }
    
    public string Message { get; set; }
    public bool IsComplated { get; set; }
}
