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

namespace TS.Core.Services
{
    public class PagamentoService : BaseService, IPagamento
    {
        private readonly IConfiguration Configuration;

        public PagamentoService(IConfiguration configuration,
            INotificador notificador) : base(notificador)
        {
            Configuration = configuration;
        }

        public bool ProcessarPagamento(int idUsuario, int idCartela)
        {
            throw new NotImplementedException();
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

                channel.BasicPublish(exchange: string.Empty,
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