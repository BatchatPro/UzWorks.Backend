namespace UzWorks.Core.Abstract;

public interface IEnvironmentAccessor
{
    string GetFullName();
    string GetWebRootPath();
    bool HasRole(string role);
    bool IsAuthorOrAdmin(Guid id);
    bool IsAuthorOrSupervisor(Guid? id);
    string GetUserId();
    string GetUserName();
}