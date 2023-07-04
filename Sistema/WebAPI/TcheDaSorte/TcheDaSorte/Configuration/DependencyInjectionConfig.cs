//using TS.API.Extensions;
//using TS.API.Interfaces;
//using TS.Core.Interfaces;
//using TS.Core.Notificacoes;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TS.API.Extensions;
using TS.API.Interfaces;
using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Data.Repository;

namespace TS.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TSContext>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICartelaRepository, CartelaRepository>();
            services.AddScoped<IPremioRepository, PremioRepository>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }   
    }
}
