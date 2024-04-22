using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using UzWorks.Core.AccessConfigurations;
using UzWorks.Identity.ClaimsPrincipalFactory;
using UzWorks.Identity.Models;
using UzWorks.Identity.Services.Auth;
using UzWorks.Identity.Services.Roles;
using UzWorks.Identity.SMS;

namespace UzWorks.Identity;

public static class IdentityModule
{
    public static IServiceCollection RegisterIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UzWorksIdentityDbContext>(option =>
        {
            option.UseNpgsql(configuration.GetConnectionString("PostgresConnectionString"), opt =>
                opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        });

        services.AddIdentity<User, Role>(option =>
         {
             option.Password.RequiredLength = 8;
             option.Password.RequireNonAlphanumeric = false;
             option.Password.RequireLowercase = true;
             option.Password.RequireUppercase = false;
             option.Password.RequireDigit = true;
             option.SignIn.RequireConfirmedPhoneNumber = true;
         }).AddRoles<Role>()
          .AddUserManager<UserManager<User>>()
          .AddRoleManager<RoleManager<Role>>()
          .AddEntityFrameworkStores<UzWorksIdentityDbContext>()
          .AddClaimsPrincipalFactory<UzWorksClaimsPrincipalFactory>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["AccessConfiguration:Audience"],
                ValidIssuer = configuration["AccessConfiguration:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey.TheSecretKey))
            };
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISmsSender, SmsSender>();

        services.AddSingleton<EskizTokenHandler>();

        services.AddOptions<SmsClientOptions>().Bind(configuration.GetSection(SmsClientOptions.SmsSectionName));

        using var provider = services.BuildServiceProvider();

        var dbContext = provider.GetService<UzWorksIdentityDbContext>();

        dbContext?.Database.Migrate();

        return services;
    }
}
