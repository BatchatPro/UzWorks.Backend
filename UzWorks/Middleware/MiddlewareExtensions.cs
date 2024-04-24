using Microsoft.AspNetCore.Identity;
using UzWorks.BL.Services.JobCategories;
using UzWorks.BL.Services.Locations.Districts;
using UzWorks.BL.Services.Locations.Regions;
using UzWorks.Identity.Models;

namespace UzWorks.API.Middleware;

public static class MiddlewareExtensions
{
    public static async Task UseRoleInitializerMiddleware(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                await RoleInitializer.InitializeAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }

        }
    }

    public static async Task UseLocationInitializerMiddleware(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var regionsService = services.GetRequiredService<IRegionsService>();
                var districtService = services.GetRequiredService<IDistrictService>();
                await LocationInitializer.InitializeAsync(regionsService, districtService);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }

        }
    }

    public static async Task UseJobCategoryInitializerMiddleware(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var jobCategoryService = services.GetRequiredService<IJobCategoryService>();
                await JobCategoryInitializerMiddleware.InitializeJobCategory(jobCategoryService);
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
