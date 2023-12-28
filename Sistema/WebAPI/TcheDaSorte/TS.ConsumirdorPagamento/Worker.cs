using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using TS.Core.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TS.Model.ViewModels;
using TS.Model.Interfaces;

namespace TS.ConsumirdorPagamento
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceProvider services { get; }

        //private readonly IConfiguration Configuration;
        //private readonly ICartelaService _cartelaService;

        public Worker(ILogger<Worker> logger,
            //IConfiguration configuration,
            //ICartelaService cartelaService,
            IServiceProvider services)
        {
            _logger = logger;
            this.services = services;
            //Configuration = configuration;
            //_cartelaService = cartelaService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var factoryScoped = services.GetRequiredService<IServiceScopeFactory>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //var URLRabbit = Configuration["Rabbit:URL"];

                    //var factory = new ConnectionFactory { HostName = URLRabbit };
                    //using var connection = factory.CreateConnection();
                    //using var channel = connection.CreateModel();

                    //var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += async (model, ea) =>
                    //{
                    //    try
                    //    {
                    //        var body = ea.Body.ToArray();
                    //        var message = Encoding.UTF8.GetString(body);

                    //        JsonSerializerOptions options = new()
                    //        {
                    //            ReferenceHandler = ReferenceHandler.Preserve,
                    //            WriteIndented = true
                    //        };

                    //        var cartelaFila = JsonSerializer.Deserialize<CartelaViewModel>(message, options);

                    //        Console.WriteLine($" [x] Received {cartelaFila.Id},{cartelaFila.Codigo}, usuario {cartelaFila.UsuarioId} ");

                    //        using (var scope = services.CreateScope())
                    //        {
                    //            var service = scope.ServiceProvider.GetRequiredService<ICartelaService>();

                    //            var cartela = await service.ObterCartelaPorId(cartelaFila.Id);

                    //            cartela.ConfirmarPagamento();

                    //            await _cartelaService.Atualizar(cartela);
                    //        }

                    //        Console.WriteLine($" COMPRA APROVADA E EMAIL ENVIADO PRO CLIENTE ");

                    //        channel.BasicAck(ea.DeliveryTag, false);
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Console.WriteLine($" FALHA : {e.Message}");
                    //        //channel.BasicNack(ea.DeliveryTag, false, true);
                    //    }
                    //};

                    //channel.BasicConsume(queue: "TS.Pagamento",
                    //                     autoAck: false,
                    //                     consumer: consumer);



                    using (var scope = services.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IPagamentoService>();

                        await service.ProcessarPagamentos();
                        //await Task.Delay(10000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}