using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Abstract;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Users;
using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.Exceptions;
using UzWorks.Identity.Models;
using UzWorks.Core.DataTransferObjects.Roles;

namespace UzWorks.Identity.Services.Roles;

public class UserService : IUserService
{
    private readonly UzWorksIdentityDbContext _dbContext;
    private readonly IEnvironmentAccessor _environmentAccessor;
    private readonly IMappingService _mappingService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(
        UzWorksIdentityDbContext dbContext, IMappingService mappingService, 
        UserManager<User> userManager, RoleManager<Role> roleManager, 
        IEnvironmentAccessor environmentAccessor)
    {
        _dbContext = dbContext;
        _mappingService = mappingService;
        _userManager = userManager;
        _roleManager = roleManager;
        _environmentAccessor = environmentAccessor;
    }

    public async Task<UserRolesDto> GetUserRoles(Guid userId)
    {
        if (await _userManager.FindByIdAsync(userId.ToString()) == null)
            throw new UzWorksException("User not found.");
        
        var userRolesDto = new UserRolesDto();
        var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId.ToString()));
        var roles = new List<RoleDto>();

        foreach (var role in userRoles)
        {
            var userRole = await _roleManager.FindByNameAsync(role);
            roles.Add(new RoleDto(userRole.Name));
        }

        userRolesDto.UserId = userId;
        userRolesDto.Roles = roles;

        return userRolesDto;
    }

    public async Task<UserRolesDto> AddRolesToUser(UserRolesDto userRolesDto)
    {
        var user = await _userManager.FindByIdAsync(userRolesDto.UserId.ToString());
        IList<string> addingRoles = new List<string>();
        
        foreach (var role in userRolesDto.Roles)
            addingRoles.Add(role.Name);

        var result = await _userManager.AddToRolesAsync(user, addingRoles);

        if (!result.Succeeded)
            throw new UzWorksException(result.Errors.Select(x => x.Description).ToString());

        _dbContext.SaveChanges();

        var userRoles = await _userManager.GetRolesAsync(user);
        var rolesDTOs = new List<RoleDto>();
        
        foreach (var role in userRoles)
        {
            var userRole = await _roleManager.FindByNameAsync(role);
            rolesDTOs.Add(new RoleDto(userRole.Name));
        }

        userRolesDto.Roles = rolesDTOs;
        
        return userRolesDto;
    }
    
    public async Task<UserRolesDto> DeleteRolesFromUser(UserRolesDto userRolesDto)
    {
        var user = await _userManager.FindByIdAsync(userRolesDto.UserId.ToString());
        var removingRoles = new List<string>();

        foreach (var role in userRolesDto.Roles)
            removingRoles.Add(role.Name);

        var result = await _userManager.RemoveFromRolesAsync(user, removingRoles);

        if (!result.Succeeded)
            throw new UzWorksException(result.Errors.Select(x => x.Description).ToString());

        _dbContext.SaveChanges();

        var userRoles = await _userManager.GetRolesAsync(user);
        var roles = new List<RoleDto>();

        foreach (var role in userRoles)
        {
            var userRole = await _roleManager.FindByNameAsync(role);
            roles.Add(new RoleDto(userRole.Name));
        }

        userRolesDto.Roles = roles;

        return userRolesDto;
    }

    public async Task Create(UserDto userDto)
    {
        if (userDto.RoleName != RoleNames.Employer && userDto.RoleName != RoleNames.Employee)
            throw new UzWorksException(
                $"Wrong Role! You have to select '{RoleNames.Employee}' or '{RoleNames.Employer}'."
                );

        if (await _userManager.FindByNameAsync(userDto.PhoneNumber) != null)
            throw new UzWorksException("This user already created.");
        
        var newUser = _mappingService.Map<User,UserDto>(userDto) ;
        var result = await _userManager.CreateAsync(newUser, userDto.Password);

        if (!result.Succeeded)
            throw new UzWorksException($"Didn't Succeeded.");

        await _userManager.AddToRolesAsync(newUser, new string[] 
        { 
            RoleNames.NewUser, 
            userDto.RoleName 
        });

        _dbContext.SaveChanges();
    }

    public async Task<UserVM> GetById(Guid id)
    {
        if (!_environmentAccessor.IsAuthorOrSupervisor(id))
            throw new UzWorksException("You you have not access for view information about this user.");

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToString(id)) ??
            throw new UzWorksException("User not found.");

        return _mappingService.Map<UserVM, User>(user);
    }
    
    public async Task<UserVM> Update(UserEM userEM)
    {
        var user = await _userManager.FindByIdAsync(userEM.Id.ToString()) ??
            throw new UzWorksException("User not Found");

        if (!_environmentAccessor.IsAuthorOrAdmin(userEM.Id))
            throw new UzWorksException("You have not access for change this user information.");

        if (user.UserName != userEM.PhoneNumber)
            throw new UzWorksException("You can not change UserName.");

        var userNewData = _mappingService.Map(userEM, user);

        var result = await _userManager.UpdateAsync(userNewData);

        if (!result.Succeeded)
            throw new UzWorksException("It is not Succeeded.");

        _dbContext.SaveChanges();

        return _mappingService.Map<UserVM, User>(userNewData) ?? 
            throw new UzWorksException("There are some errors.");
    }

    public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId.ToString()) ??
            throw new UzWorksException("User not found.");

        if (!_environmentAccessor.IsAuthorOrAdmin(resetPasswordDto.UserId))
            return false;

        var result = await _userManager.ChangePasswordAsync(user, resetPasswordDto.OldPassword, resetPasswordDto.NewPassword);

        if (!result.Succeeded)
            throw new UzWorksException(result.Errors.Select(x => x.Description).ToString());

        _dbContext.SaveChanges();

        return true;
    }

    public async Task<bool> ResetPassword(Guid userId, string newPassword)
    {
        if (!_environmentAccessor.IsAdmin(Guid.Parse(_environmentAccessor.GetUserId())))
            throw new UzWorksException("You cannot change user password");

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToString(userId)) ??
            throw new UzWorksException("User not found.");

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, newPassword);

        _dbContext.SaveChanges();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToString(id));
        
        if (user == null || !_environmentAccessor.IsAuthorOrAdmin(id))
            return false;
        
        await _userManager.DeleteAsync(user);
        _dbContext.SaveChanges();

        return true;

    }

    public async Task<IEnumerable<UserVM>> GetAll(
        int pageNumber, int pageSize, 
        string? gender, string? email, 
        string? phoneNumber)
    {
        var query = _dbContext.Users.AsQueryable();

        if(gender is not null)
            query = query.Where(x => x.Gender == gender);

        if(email is not null)
            query = query.Where(x => x.Email == email);

        if (phoneNumber is not null)
            query = query.Where(x => x.PhoneNumber == phoneNumber);

        if (pageNumber != 0 && pageSize != 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        var users = await query.ToArrayAsync();
        var result = _mappingService.Map<IEnumerable<UserVM>, IEnumerable<User>>(users);

        return result;
    }

    public async Task<string> GetUserFullName(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()) ??
            throw new UzWorksException("User not found.");

        return user.FirstName + " " + user.LastName;
    }
}


