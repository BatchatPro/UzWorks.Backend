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

namespace UzWorks.Identity.Services.Roles;

public class UserService : IUserService
{
    private readonly UzWorksIdentityDbContext _dbContext;
    private readonly IMappingService _mappingService;
    private readonly UserManager<User> _userManager;
    public UserService(UzWorksIdentityDbContext dbContext, IMappingService mappingService, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _mappingService = mappingService;
        _userManager = userManager;
    }

    public Task AddRolesToUser(IEnumerable<UserRolesDto> userRoles)
    {
        throw new NotImplementedException();
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

    public Task DeleteRolesFromUser(IEnumerable<UserRolesDto> userRoles)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserVM>> GetAll(int pageNumber, int pageSize, string? gender, string? email, string? phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(UserEM userEM)
    {
        throw new NotImplementedException();
    }
}


