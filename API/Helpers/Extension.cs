using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers;
public static class Extension
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options=>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin() //WithOrigins("http://domain.com")
                .AllowAnyMethod() //WithMethods("GET", "POST")
                .AllowAnyHeader() //WithHeaders("accept", "content-type")
            );
        });

    public static void AddAplicationServices(this IServiceCollection services) =>
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
}