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

        public async Task ProcessarPagamento(CartelaCompraViewModel cartelaFila)
        {
            try
            {
                var cartela = await _cartelaService.ObterCartelaPorId(cartelaFila.Id);

                cartela.ConfirmarPagamento();

                await _cartelaService.Atualizar(cartela);
            }
            catch (Exception e)
            {
                Console.WriteLine($" FALHA : {e.Message}");
            }
        }

        public bool PublicarPagamento(Cartela cartela)
        {
            try
            {
                var URLRabbit = Configuration["Rabbit:URL"];

                var factory = new ConnectionFactory { HostName = URLRabbit };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                var cartelaCompra = new CartelaCompraViewModel(cartela.Id, 
                                        cartela.Codigo,
                                        cartela.PremioId,
                                        cartela.UsuarioId.Value);

                string message = JsonSerializer.Serialize(cartelaCompra, options);
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