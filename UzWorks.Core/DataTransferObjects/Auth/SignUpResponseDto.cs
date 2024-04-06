namespace UzWorks.Core.DataTransferObjects.Auth;

public class SignUpResponseDto
{ 
    public SignUpResponseDto(Guid id, string userName, string firstName, string lastName, List<string> roles)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Roles = roles;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<string>? Roles { get; set; }
}
