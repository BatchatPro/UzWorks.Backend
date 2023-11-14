using Microsoft.AspNetCore.Identity;
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
}
