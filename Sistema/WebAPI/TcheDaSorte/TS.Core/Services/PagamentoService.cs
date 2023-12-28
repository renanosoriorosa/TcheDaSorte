using FluentValidation;
using FluentValidation.Results;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using TS.Core.Interfaces;
using TS.Core.Notificacoes;
using TS.Model.Models;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using TS.Model.Interfaces;
using TS.Model.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace TS.Core.Services
{
    public class PagamentoService : BaseService, IPagamentoService
    {
        private readonly IConfiguration Configuration;
        private readonly ICartelaService _cartelaService;
        public IServiceProvider services { get; }

        public PagamentoService(IConfiguration configuration,
            ICartelaService cartelaService,
            IServiceProvider services,
            INotificador notificador) : base(notificador)
        {
            Configuration = configuration;
            _cartelaService = cartelaService;
            this.services = services;
        }

        public async Task ProcessarPagamentos()
        {
            var URLRabbit = Configuration["Rabbit:URL"];

            var factory = new ConnectionFactory { HostName = URLRabbit };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

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

                    var cartelaFila = JsonSerializer.Deserialize<CartelaViewModel>(message, options);

                    Console.WriteLine($" [x] Received {cartelaFila.Id},{cartelaFila.Codigo}, usuario {cartelaFila.UsuarioId} ");
                    
                    using (var scope = services.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<ICartelaService>();

                        var cartela = await service.ObterCartelaPorId(cartelaFila.Id);

                        cartela.ConfirmarPagamento();

                        await _cartelaService.Atualizar(cartela);
                    }

                    Console.WriteLine($"COMPRA APROVADA E EMAIL ENVIADO PRO CLIENTE ");

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine($" FALHA : {e.Message}");
                    //channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queue: "TS.Pagamento",
                                 autoAck: false,
                                 consumer: consumer);
        }

        public bool PublicarPagamento(Cartela cartela)
        {
            try
            {
                var URLRabbit = Configuration["Rabbit:URL"];

                var factory = new ConnectionFactory { HostName = URLRabbit };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                //channel.QueueDeclare(queue: "Pagamento",
                //                     durable: false,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: null);

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string message = JsonSerializer.Serialize(cartela, options);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "TSExchange",
                                     routingKey: "TS.Pagamento",
                                     basicProperties: null,
                                     body: body);

                return true;
            }
            catch (Exception e)
            {
                Notificar($"Falha ao publicar o pagamento. ERRO: {e.Message}");
                return false;
            }
        }
    }
}