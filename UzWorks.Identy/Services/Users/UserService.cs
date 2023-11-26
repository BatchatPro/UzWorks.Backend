using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UzWorks.Core.Abstract;
using UzWorks.Core.Constants;
using UzWorks.Core.DataTransferObjects.Auth;
using UzWorks.Core.DataTransferObjects.Roles;
using UzWorks.Core.DataTransferObjects.UserRoles;
using UzWorks.Core.Exceptions;
using UzWorks.Identity.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UzWorks.Identity.Services.Roles;

public class UserService : IUserService
{
    private readonly UzWorksIdentityDbContext _dbContext;
    private readonly IMappingService _mappingService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(UzWorksIdentityDbContext dbContext, IMappingService mappingService, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _dbContext = dbContext;
        _mappingService = mappingService;
        _userManager = userManager;
        _roleManager = roleManager;
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
        if (userDto.RoleNmae != RoleNames.Employer && userDto.RoleNmae != RoleNames.Employee)
            throw new UzWorksException($"Wrong Role! You have to select '{RoleNames.Employee}' or '{RoleNames.Employer}'.");

        var user = await _userManager.FindByNameAsync(userDto.UserName);

        if (user != null)
            throw new UzWorksException("This user already created.");
        
        var newUser = new User(userDto.FirstName, userDto.LastName, userDto.UserName);
        var result = await _userManager.CreateAsync(newUser, userDto.Password);

        if (!result.Succeeded)
            throw new UzWorksException($"Didn't Succeeded.");

        await _userManager.AddToRolesAsync(newUser, new string[] { RoleNames.NewUser, userDto.RoleNmae });

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        if (user != null)
            await _userManager.DeleteAsync(user);

        await _dbContext.SaveChangesAsync();
    }

    public Task<IEnumerable<UserVM>> GetAll(int pageNumber, int pageSize, string? gender, string? email, string? phoneNumber)
    {
        var query = _dbContext.Set<User>().AsQueryable();

        if(gender is not null)
            query = query.Where(x => x.Gender == gender);

        if(email is not null)
            query = query.Where(x => x.Email == email);

        if (phoneNumber is not null)
            query = query.Where(x => x.PhoneNumber == phoneNumber);

        if (pageNumber != 0 && pageSize != 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        return (Task<IEnumerable<UserVM>>)query;
    }

    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        return _mappingService.Map<UserDto, User>(user);
    }

    public async Task<UserVM> Update(UserEM userEM)
    {
        var _user = await _userManager.FindByNameAsync(userEM.UserName);
        
        if (await _userManager.FindByIdAsync(userEM.Id.ToString()) == null)
            throw new UzWorksException("User not Found");

        var newUserData = _mappingService.Map<User, UserEM>(userEM);
        var result = await _userManager.UpdateAsync(newUserData);

        if (!result.Succeeded)
            throw new UzWorksException("It is not Succeeded.");

        if (newUserData != null)
        {
            await _userManager.RemovePasswordAsync(newUserData);
            await _userManager.AddPasswordAsync(newUserData, userEM.Password);
        }

        return _mappingService.Map<UserVM, User>(newUserData);
    }
}


