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
    [Authorize]
    [Route("v{version:apiVersion}/[controller]/[action]")]
    public class CartelaController : MainController
    {
        private readonly ICartelaService _cartelaService;

        public CartelaController(INotificador notificador,
            IUser user,
            ICartelaService cartelaService) : base(notificador, user)
        {
            _cartelaService = cartelaService;
        }

        [HttpGet("{idUsuario:int}")]
        public async Task<ActionResult> ObterCartelasDoUsuario(int idUsuario)
        {
            var cartelas = await _cartelaService.ObterTodosPorPremioIdUsuario(idUsuario);

            return CustomResponse(cartelas);
        }

        [HttpGet("{idCartela:int}")]
        public async Task<ActionResult> ObterDetalhesDaCartela(int idCartela)
        {
            var cartelas = await _cartelaService.ObterTodosOsDadosPorId(idCartela);

            return CustomResponse(cartelas);
        }
    }
}
