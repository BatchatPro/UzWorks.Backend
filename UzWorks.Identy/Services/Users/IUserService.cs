using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.DataTransferObjects.Users;
using UzWorks.Core.Enums.GenderTypes;

namespace UzWorks.Identity.Services.Roles;

public interface IUserService
{
    Task Create(UserDto userDto);
    Task<bool> Delete(Guid id);
    Task<UserVM> Update(UserEM userEM);
    Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
    Task<bool> ResetPassword(Guid userId, string NewPassword);
    Task<UserRolesDto> GetUserRoles(Guid id);
    Task<UserRolesDto> AddRolesToUser(UserRolesDto userRoles);
    Task<UserRolesDto> DeleteRolesFromUser(UserRolesDto userRoles);
    Task<IEnumerable<UserVM>> GetAll(int pageNumber, int pageSize, Gender? gender, string? email, string? phoneNumber);
    Task<UserVM> GetById(Guid id);
    Task<string> GetUserFullName(Guid id);
}
