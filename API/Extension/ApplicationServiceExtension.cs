using Domain.Entities;
using Domain.Interfaces;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Identity;
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
    services.AddScoped<IUnitOfWork, UnitOfWork>(); 
    }
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        
    }
    public static void AddValidationErrors(this IServiceCollection services)
    {

    }
    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        
    }
}