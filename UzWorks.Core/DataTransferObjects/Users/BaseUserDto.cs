using NullGuard;

namespace UzWorks.Core.DataTransferObjects.Users;

public class BaseUserDto
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [AllowNull]
    public string? Email { get; set; }
    [AllowNull]
    public string? PhoneNumber { get; set; }
    [AllowNull]
    public string? Gender { get; set; }
    [AllowNull]
    public string? MobileId { get; set; }
    [AllowNull]
    public DateTime BirthDate { get; set; }
}

