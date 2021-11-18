using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using ToolBox.Security.Configuration;
using ToolBox.Security.Services;

namespace ToolBox.Security.DependencyInjection.Extensions
{
    public static class JwtDependencyInjectionExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfigurationSection section)
        {
            JwtConfiguration config = section.Get<JwtConfiguration>();
            return services.AddJwt(config);
        }

        public static IServiceCollection AddJwt(this IServiceCollection services, JwtConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<JwtService>();
            services.AddScoped<JwtSecurityTokenHandler>();
            return services;
        }
    }
}
