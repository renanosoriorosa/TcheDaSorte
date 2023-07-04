//using TS.API.Extensions;
//using TS.API.Interfaces;
//using TS.Core.Interfaces;
//using TS.Core.Notificacoes;
//using TS.Core.Services;
using TS.Data.Context;
//using TS.Data.Interfaces;
//using TS.Data.Repository;
//using Microsoft.Extensions.Options;
//using Swashbuckle.AspNetCore.SwaggerGen;

namespace TS.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TSContext>();

            //services.AddScoped<INotificador, Notificador>();
            //services.AddScoped<IProdutoService, ProdutoService>();
            //services.AddScoped<IProdutoRepository, ProdutoRepository>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IUser, AspNetUser>();

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }   
    }
}
