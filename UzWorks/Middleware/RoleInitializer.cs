﻿using Microsoft.AspNetCore.Identity;
using UzWorks.Identity.Models;
using UzWorks.Core.Constants;

namespace UzWorks.API.Middleware;

 public class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        string SuperAdminEmail = "goblindev02@gmail.com";
        string SuperAdminPassword = "123456dev";

        var Roles = new Dictionary<string, string>()
        {
            { RoleNames.SuperAdmin, "SuperAdmin" },
            { RoleNames.Supervisor, "Supervisor" },
            { RoleNames.Employer, "Employer" },
            { RoleNames.Employee, "Employee" },
            { RoleNames.NewUser, "NewUser" },
        };

        foreach (var role in Roles)
            if (await roleManager.FindByNameAsync(role.Key) == null)
                await roleManager.CreateAsync(new Role(role.Key, role.Value));

        User user = await userManager.FindByNameAsync(SuperAdminEmail);

        if (user != null) 
        {
            await userManager.AddToRolesAsync(user, new string[] {
                RoleNames.Employee,
                RoleNames.Employer,
                RoleNames.SuperAdmin,
                RoleNames.Supervisor
            });
        }

        else
        {
            user = new User(
                "Abdulaziz", "Nabijonov", SuperAdminEmail, SuperAdminEmail, "Male", new DateTime(2002, 06, 17)
            );
            
            IdentityResult result = await userManager.CreateAsync(user, SuperAdminPassword);
            
            if (result.Succeeded)
                await userManager.AddToRolesAsync(user, new string[] {
                    RoleNames.Employee,
                    RoleNames.Employer,
                    RoleNames.SuperAdmin,
                    RoleNames.Supervisor
                });

        }
    }
}