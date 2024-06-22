using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.DataTransferObjects.Users;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Identity.Services.Roles;

public interface IUserService
{
    Task Create(UserDto userDto);
    Task<IEnumerable<UserVM>> GetAll(int pageNumber, int pageSize, GenderEnum? gender, string? email, string? phoneNumber);
    Task<UserVM> GetById(Guid id);
    Task<string> GetUserFullName(Guid id);
    Task<UserRolesDto> GetUserRoles(Guid id);
    Task<int> GetCount();
    Task<UserVM> Update(UserEM userEM);
    Task<UserRolesDto> AddRolesToUser(UserRolesDto userRoles);
    Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
    Task<bool> ResetPassword(Guid userId, string NewPassword);
    Task<bool> Delete(Guid id);
    Task<UserRolesDto> DeleteRolesFromUser(UserRolesDto userRoles);
}
