using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using UzWorks.Core.Abstract;
using UzWorks.Core.Checkers;
using UzWorks.Infrastructure.Mappers;

namespace UzWorks.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection RegisterInfrastructureModule(this IServiceCollection services, IConfiguration configs)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddSingleton<IMappingService, MappingService>();
        services.AddScoped<PhoneNumberService>();

        services.AddSendGrid(options =>
        {
            options.ApiKey = configs["SMTP:ApiKey"];
        });

        services.AddMemoryCache();

        return services;
    }
}
