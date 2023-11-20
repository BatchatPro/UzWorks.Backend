﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UzWorks.Persistence.Data;
using System.Reflection;
using UzWorks.Persistence.Repositories;
using UzWorks.Persistence.Repositories.Regions;
using UzWorks.Persistence.Repositories.Districts;
using UzWorks.Persistence.Repositories.JobCategories;
using UzWorks.Persistence.Repositories.Jobs;
using UzWorks.Persistence.Repositories.Workers;

namespace UzWorks.Persistence;

public static class PersistenceModule
{
    public static IServiceCollection RegisterPersistenceModule(this IServiceCollection services, IConfiguration configs)
    {
        services.AddDbContext<UzWorksDbContext>(options =>
        {
            options.UseNpgsql(configs.GetConnectionString("PostgresConnectionString"), opt =>
                opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IRegionsRepository, RegionsRepository>();
        services.AddScoped<IDistrictsRepository, DistrictsRepository>();
        services.AddScoped<IJobCategoriesRepository, JobCategoriesRepository>();
        services.AddScoped<IJobsRepository, JobsRepository>();
        services.AddScoped<IWorkersRepository, WorkersRepository>();

        using var provider = services.BuildServiceProvider();

        var dbContext = provider.GetService<UzWorksDbContext>();

        //dbContext?.Database.Migrate();

        return services;
    }
}
