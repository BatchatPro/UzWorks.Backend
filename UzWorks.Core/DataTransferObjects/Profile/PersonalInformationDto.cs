namespace UzWorks.Core.DataTransferObjects.Profile;

public class PersonalInformationDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public string Salary { get; set; }
    public DateTime BirthDate { get; set; }
    public string Destrict { get; set; }
    public string Region { get; set; }
}
