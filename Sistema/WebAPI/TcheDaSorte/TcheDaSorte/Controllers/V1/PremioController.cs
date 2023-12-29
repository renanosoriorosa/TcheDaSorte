using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TS.API.Extensions;
using TS.API.Interfaces;
using TS.Model.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TS.Core.Interfaces;
using TS.Model.Models;
using AutoMapper;
using TS.Model.Interfaces;

namespace TS.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("v{version:apiVersion}/[controller]/[action]")]
    public class PremioController : MainController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPremioService _premioService;
        private readonly ICartelaService _cartelaService;

        public PremioController(INotificador notificador,
            IUser user,
            IMapper mapper,
            IPremioService premioService,
            ICartelaService cartelaService,
            ILogger<PremioController> logger) : base(notificador, user)
        {
            _logger = logger;
            _premioService = premioService;
            _mapper = mapper;
            _cartelaService = cartelaService;
        }

        [HttpPost(Name = "Cadastrar")]
        public async Task<ActionResult> Cadastrar(CadastroPremioViewModel cadastroPremio)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var premio = new Premio(cadastroPremio.Codigo, cadastroPremio.DataEvento, cadastroPremio.ValorPremio);

            await _premioService.Adicionar(premio);

            if (!OperacaoValida())
                return CustomResponse();

            await _cartelaService.CriarCartelasParaPremio(premio.Id, cadastroPremio.NumeroCartelas, cadastroPremio.PrecoCartela);

            if (!OperacaoValida())
            {
                //remove as cartelas do premio
                await _cartelaService.RemoverTodasAsCartelasDoPremio(premio.Id);

                // remove o premio
                await _premioService.Remover(premio.Id);
            }

            return CustomResponse();
        }

        [HttpGet]
        public async Task<ActionResult> ListaPremiosDisponiveis(
            [FromQuery] int pagina = 1, 
            [FromQuery] int tamanhoPagina = 10)
        {
            if (tamanhoPagina > 100)
                return SendBadRequest("Tamanho da página é muito grande.");

            var premios = await _premioService.ObterPremiosDisponiveisAsNoTracking(pagina, tamanhoPagina);

            return CustomResponse(premios);
        }

        [HttpGet("{idPremio:int}")]
        public async Task<ActionResult> ObterPremio(int idPremio)
        {
            var premio = await _premioService.ObterPorIdAsNoTracking(idPremio);

            if (premio == null)
                return SendBadRequest($"O prêmio com o id {idPremio} não foi encontrado.");

            premio.Cartelas.AddRange(await _cartelaService.ObterTodosPorPremioId(idPremio));

            return CustomResponse(premio);
        }
    }
}
