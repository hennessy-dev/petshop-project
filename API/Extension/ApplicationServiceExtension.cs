using Domain.Entities;
using Domain.Interfaces;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using API.Services;
using API.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
namespace API.Extension;

public static class ApplicationServiceExtension 
{
    public static void ConfigureCors(this IServiceCollection services) =>
    services.AddCors(options => 
    {
        options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()  //WhithOrigins("http://domain.com")
            .AllowAnyHeader()         //WithHeaders("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader());       //WithMehods("accept", "content-type")
    });
    public static void AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>(); 
    }
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        //Tomar configuracion de AppSettings
        services.Configure<JWT>(configuration.GetSection("JWT"));
        //Agregando autenticacion - JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
    }
    public static void AddValidationErrors(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {

                var errors = actionContext.ModelState.Where(u => u.Value.Errors.Count > 0)
                                                .SelectMany(u => u.Value.Errors)
                                                .Select(u => u.ErrorMessage).ToArray();

                var errorResponse = new ApiValidation()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
    }
    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new()
                    {
                        Endpoint = "*",
                        Limit = 2,
                        Period = "15s"
                    }
                };
            });
    }
}
