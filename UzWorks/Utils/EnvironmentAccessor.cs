using System.Security.Claims;
using UzWorks.Core.Abstract;
using UzWorks.Core.Constants;
using UzWorks.Core.Exceptions;
using UzWorks.Identity.Constants;

namespace UzWorks.API.Utils;

public class EnvironmentAccessor : IEnvironmentAccessor
{
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _contextAccessor;

    public EnvironmentAccessor(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
    {
        _environment = environment;
        _contextAccessor = contextAccessor;
    }

    public string GetFullName()
    {
        throw new NotImplementedException();
    }

    public string GetWebRootPath()
    {
        return _environment.WebRootPath;
    }

    public bool HasRole(string role)
    {
        throw new NotImplementedException();
    }

    public bool IsAuthorOrAdmin(Guid id)
    {
        if (_contextAccessor.HttpContext is null)
            throw new UzWorksException("HttpContext can not be null.");

        if (_contextAccessor.HttpContext.User.IsInRole(RoleNames.SuperAdmin) || GetUserId() == id.ToString())
            return true;

        return false;
    }

    public bool IsAuthorOrSupervisor(Guid? id)
    {
        if (_contextAccessor.HttpContext is null)
            throw new UzWorksException("HttpContext can not be null.");

        if (_contextAccessor.HttpContext.User.IsInRole(RoleNames.Supervisor) || GetUserId() == id.ToString())
            return true;

        return false;
    }

    public string GetUserId()
    {
        return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimNames.UserId.ToString()))?.Value;
    }

    public string GetUserName()
    {
        return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimNames.UserName.ToString()))?.Value;
    }
}

