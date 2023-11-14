using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UzWorks.API.Middleware;
using UzWorks.Core.AccessConfigurations;
using UzWorks.Identity;
using UzWorks.Identity.ClaimsPrincipalFactory;
using UzWorks.Identity.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var connectionString = configuration["ConnectionStrings:PostgresConnectionString"];

builder.Services.AddOptions();
builder.Services.Configure<AccessConfiguration>(configuration.GetSection("AccessConfiguration"));
builder.Services.AddDbContext<UzWorksIdentityDbContext>(option => option.UseNpgsql(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentity<User, Role>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = false;
    option.Password.RequireDigit = true;
}).AddRoles<Role>()
.AddUserManager<UserManager<User>>()
.AddRoleManager<RoleManager<Role>>()
.AddEntityFrameworkStores<UzWorksIdentityDbContext>()
.AddClaimsPrincipalFactory<UzWorksClaimsPrincipalFactory>();

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

builder.Services.AddAuthentication(options =>
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

await app.UseRoleInitializerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
