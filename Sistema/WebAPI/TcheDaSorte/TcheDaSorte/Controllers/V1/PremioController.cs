using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TS.API.Extensions;
using TS.API.Interfaces;
using TS.ViewModels.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TS.Core.Interfaces;
using TS.Model.Models;
using AutoMapper;

namespace TS.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    //[ApiVersion("1.0", Deprecated = true)] FUTURO
    [Route("v{version:apiVersion}/[controller]/[action]")]
    public class PremioController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPremioService _premioService;

        public PremioController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings, 
            INotificador notificador, 
            IUser user,
            IMapper mapper,
            IPremioService premioService,
            ILogger<PremioController> logger) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _premioService = premioService;
            _mapper = mapper;
        }

        [HttpPost(Name = "Cadastrar")]
        public async Task<ActionResult> Cadastrar(CadastroPremioViewModel cadastroPremio)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var premio = new Premio(cadastroPremio.Codigo, cadastroPremio.DataEvento, cadastroPremio.ValorPremio);

            await _premioService.Adicionar(premio);



            return CustomResponse();
        }
    }
}
