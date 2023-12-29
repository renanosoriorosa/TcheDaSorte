using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TS.ConsumirdorPagamento;
using TS.Core.Interfaces;
using TS.Core.Notificacoes;
using TS.Core.Services;
using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Data.Repository;
using TS.Model.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<TSContext>(c =>
                         c.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

        services.AddHostedService<Worker>();
        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<ICartelaService, CartelaService>();
        services.AddScoped<ICartelaRepository, CartelaRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<TSContext>();

        services.AddScoped<INotificador, Notificador>();

        services.AddAutoMapper(typeof(Program));
    })
    .Build();

await host.RunAsync();
