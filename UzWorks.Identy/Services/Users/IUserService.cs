using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.DataTransferObjects.Roles;

namespace UzWorks.Identity.Services.Roles;

public interface IUserService
{
    Task Create(UserDto userDto);
    Task Delete(Guid id);
    Task Update(UserEM userEM);
    Task AddRolesToUser(IEnumerable<UserRolesDto> userRoles);
    Task DeleteRolesFromUser(IEnumerable<UserRolesDto> userRoles);
    Task<IEnumerable<UserVM>> GetAll(int pageNumber, int pageSize, string? gender, string? email, string? phoneNumber);
    Task<UserDto> GetById(Guid id);
}
