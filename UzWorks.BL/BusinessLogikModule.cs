using Microsoft.Extensions.DependencyInjection;
using UzWorks.BL.Services.Contacts;
using UzWorks.BL.Services.FAQs;
using UzWorks.BL.Services.FeedBacks;
using UzWorks.BL.Services.JobCategories;
using UzWorks.BL.Services.Jobs;
using UzWorks.BL.Services.Locations.Districts;
using UzWorks.BL.Services.Locations.Regions;
using UzWorks.BL.Services.Workers;
using UzWorks.BL.Services.Workers.Experiences;

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
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IFAQService, FAQService>();
        services.AddScoped<IFeedBackService, FeedBackService>();

        return services;
    }
}
