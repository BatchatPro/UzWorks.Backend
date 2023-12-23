namespace UzWorks.Core.DataTransferObjects.Users;

public class UserEM : BaseUserDto
{
    public Guid Id { get; set; }
    public string Password { get; set; }
}
