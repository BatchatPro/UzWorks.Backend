using Microsoft.Extensions.DependencyInjection;
using UzWorks.BL.Services.JobCategories;
using UzWorks.BL.Services.Jobs;
using UzWorks.BL.Services.Locations.Districts;
using UzWorks.BL.Services.Locations.Regions;
using UzWorks.BL.Services.Workers;

namespace UzWorks.BL;

public static class BusinessLogicModule
{
    public static IServiceCollection RegisterBLModule(this IServiceCollection services)
    {
        services.AddScoped<IJobCategoryService, JobCategoryService>();
        services.AddScoped<IRegionsService, RegionsService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IWorkerService, WorkerService>();
        
        return services;
    }
}
