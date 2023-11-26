namespace UzWorks.Core.DataTransferObjects.Roles;

public class UserEM : BaseUserDto
{
    public Guid Id { get; set; }
    public string Password { get; set; }
}
