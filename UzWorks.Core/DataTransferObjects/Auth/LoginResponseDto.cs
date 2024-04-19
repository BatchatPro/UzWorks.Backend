namespace UzWorks.Core.DataTransferObjects.Auth;

public class LoginResponseDto
{
    public LoginResponseDto(
                Guid id, string token, DateTime expiration, 
                string firstName, string lastName, 
                string gender, DateTime birthDate, 
                string phoneNumber, IList<string> roles)
    {
        Id = id;
        Token = token;
        Expiration = expiration;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Roles = roles;
    }

    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;  
    public DateTime BirthDate { get; set; }
    public IList<string>? Roles { get; set;}
}
