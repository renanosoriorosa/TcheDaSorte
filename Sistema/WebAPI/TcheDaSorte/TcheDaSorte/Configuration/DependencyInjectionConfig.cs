using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TS.API.Extensions;
using TS.API.Interfaces;
using TS.Core.Interfaces;
using TS.Core.Notificacoes;
using TS.Core.Services;
using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Data.Repository;
using TS.Model.Interfaces;
using TS.Model.Models;
using TS.Models.Models.Validations;

namespace TS.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TSContext>();

            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICartelaRepository, CartelaRepository>();
            services.AddScoped<ICartelaService, CartelaService>();
            services.AddScoped<IPremioRepository, PremioRepository>();
            services.AddScoped<IPremioService, PremioService>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<IPagamentoService, PagamentoService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }   
    }
}
