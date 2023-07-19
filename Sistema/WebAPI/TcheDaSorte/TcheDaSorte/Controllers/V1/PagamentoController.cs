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
using TS.Core.Services;

namespace TS.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    //[Authorize]
    [Route("v{version:apiVersion}/[controller]/[action]")]
    public class PagamentoController : MainController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPremioService _premioService;
        private readonly ICartelaService _cartelaService;
        private readonly IUsuarioService _usuarioService;

        public PagamentoController(INotificador notificador,
            IUser user,
            IMapper mapper,
            IPremioService premioService,
            ICartelaService cartelaService,
            IUsuarioService usuarioService,
            ILogger<PagamentoController> logger) : base(notificador, user)
        {
            _logger = logger;
            _premioService = premioService;
            _mapper = mapper;
            _cartelaService = cartelaService;
            _usuarioService = usuarioService;
        }

        [HttpPost(Name = "ComprarCartela")]
        public async Task<ActionResult> ComprarCartela(int idCartela)
        {
            var cartela = await _cartelaService.ObterCartelaPorId(idCartela);

            if (cartela == null)
                return SendBadRequest($"A cartela {idCartela} não foi encontrada.");

            if(!cartela.DisponivelPraCompra())
                return SendBadRequest($"A cartela {idCartela} não disponivel pra compra.");

            var usuario = await _usuarioService.ObterPorIdIdentity(UsuarioId.ToString());

            cartela.ReservarCartela(usuario.Id);

            //coloca na fila pra fazer a compra da cartela

            return CustomResponse();
        }

    }
}
