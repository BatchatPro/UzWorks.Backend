using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.DataTransferObjects.Roles;

namespace UzWorks.Identity.Services.Roles;

public interface IUserService
{
    Task Create(UserDto userDto);
    Task Delete(Guid id);
    Task<UserVM> Update(UserEM userEM);
    Task<UserRolesDto> AddRolesToUser(UserRolesDto userRoles);
    Task<UserRolesDto> DeleteRolesFromUser(UserRolesDto userRoles);
    Task<IEnumerable<UserVM>> GetAll(int pageNumber, int pageSize, string? gender, string? email, string? phoneNumber);
    Task<UserVM> GetById(Guid id);
}
