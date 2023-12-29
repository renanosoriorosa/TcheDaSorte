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
        private readonly IConfiguration Configuration;
        public IServiceProvider services { get; }

        public Worker(ILogger<Worker> logger,
            IConfiguration configuration,
            IServiceProvider services)
        {
            _logger = logger;
            this.services = services;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var URLRabbit = Configuration["Rabbit:URL"];

                    var factory = new ConnectionFactory { HostName = URLRabbit };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    var consumeTaskCompletionSource = new TaskCompletionSource<bool>();

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            JsonSerializerOptions options = new()
                            {
                                ReferenceHandler = ReferenceHandler.Preserve,
                                WriteIndented = true
                            };

                            var cartelaFila = JsonSerializer.Deserialize<CartelaCompraViewModel>(message, options);

                            Console.WriteLine($" [x] Received {cartelaFila.Id},{cartelaFila.Codigo}, usuario {cartelaFila.UsuarioId} ");

                            using (var scope = services.CreateScope())
                            {
                                var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
                                var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();

                                var usuario = await usuarioService.ObterPorId(cartelaFila.UsuarioId);

                                Console.WriteLine($"Processando pagamento para o usuario {usuario.Nome}...");

                                await pagamentoService.ProcessarPagamento(cartelaFila);

                                Console.WriteLine($" COMPRA APROVADA E EMAIL ENVIADO PRO CLIENTE {usuario.Nome}");
                            }

                            channel.BasicAck(ea.DeliveryTag, false);

                            consumeTaskCompletionSource.SetResult(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($" FALHA : {e.Message}");
                            channel.BasicNack(ea.DeliveryTag, false, true);
                        }
                    };

                    channel.BasicConsume(queue: "TS.Pagamento",
                                         autoAck: false,
                                         consumer: consumer);

                    await consumeTaskCompletionSource.Task;

                    // Após o consumo ser finalizado, encerra a conexão e o canal
                    connection.Close();
                    channel.Close();

                    await Task.Delay(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}