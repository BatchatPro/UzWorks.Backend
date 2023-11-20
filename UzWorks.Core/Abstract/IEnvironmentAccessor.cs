namespace UzWorks.Core.Abstract;

public interface IEnvironmentAccessor
{
    string GetFullName();
    string GetWebRootPath();
    bool HasRole(string role);

}
