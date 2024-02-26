namespace UzWorks.Core.Entities.Contacts;

public class Contact : BaseEntity
{
    public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public bool IsComplated { get; set; } = false;
}
