using Microsoft.OpenApi.Models;
using UzWorks.API.Middleware;
using UzWorks.API.Utils;
using UzWorks.BL;
using UzWorks.Core.Abstract;
using UzWorks.Core.AccessConfigurations;
using UzWorks.Identity;
using UzWorks.Infrastructure;
using UzWorks.Infrastructure.ExceptionHandling;
using UzWorks.Persistence;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddOptions();
builder.Services.Configure<AccessConfiguration>(configuration.GetSection("AccessConfiguration"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UzWorks Api", Version = "v1" });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

});

builder.Services.AddScoped<IEnvironmentAccessor, EnvironmentAccessor>();

builder.Services.RegisterIdentityModule(builder.Configuration);
builder.Services.RegisterPersistenceModule(builder.Configuration);
builder.Services.RegisterBLModule();
builder.Services.RegisterInfrastructureModule(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opt =>
    {
        opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//await app.UseRoleInitializerMiddleware();
//await app.UseLocationInitializerMiddleware();
//await app.UseJobCategoryInitializerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
