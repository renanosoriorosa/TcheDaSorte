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
using TS.Model.Interfaces;

namespace TS.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    //[ApiVersion("1.0", Deprecated = true)] FUTURO
    [Route("v{version:apiVersion}/[controller]/[action]")]
    public class AutenticacaoController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IUsuarioService _usuarioService;

        public AutenticacaoController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings, 
            INotificador notificador, 
            IUser user,
            IUsuarioService usuarioService,
            ILogger<AutenticacaoController> logger) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost(Name = "Registrar")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            if (registerUser.ConfirmPassword != registerUser.Password)
                return SendBadRequest("As senhas não conferem.");

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                //cria o usuario no banco de dados
                var usuario = new Usuario(registerUser.Nome, user.Id);
                await _usuarioService.Adicionar(usuario);

                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJWT(user.Email));
            }
            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [AllowAnonymous]
        [HttpPost(Name = "Login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuário " + loginUser.Email + " logado com sucesso");
                return CustomResponse(await GerarJWT(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("O usuário foi bloqueado temporariamente");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorretos");
            return CustomResponse(loginUser);
        }

        [HttpGet(Name = "Detalhes")]
        public async Task<ActionResult> Detalhes(string emailUsuario)
        {
            if (string.IsNullOrEmpty(emailUsuario))
            {
                NotificarErro($"informe o e-mail do usuário.");
                return CustomResponse();
            }

            var userIdentity = await _userManager.FindByEmailAsync(emailUsuario);

            if (userIdentity == null)
            {
                NotificarErro($"O usuário {emailUsuario} não foi encontrado.");
                return CustomResponse();
            }

            var usuario = await _usuarioService.ObterPorIdIdentity(userIdentity.Id);

            if (usuario == null)
            {
                NotificarErro($"O usuário {userIdentity.Id} não foi encontrado.");
                return CustomResponse();
            }

            usuario.Email = userIdentity.Email;
            usuario.IndentityId = userIdentity.Id;

            return CustomResponse(usuario);
        }

        [HttpDelete(Name = "Remover")]
        public async Task<ActionResult> Remover(string emailUsuario)
        {
            if (string.IsNullOrEmpty(emailUsuario))
            {
                NotificarErro($"informe o e-mail do usuário.");
                return CustomResponse();
            }

            var user = await _userManager.FindByEmailAsync(emailUsuario);

            if (user == null)
            {
                NotificarErro($"O usuário {emailUsuario} não foi encontrado.");
                return CustomResponse();
            }

            try
            {
                await _userManager.DeleteAsync(user);
                await _usuarioService.RemoverPorIdIdentity(user.Id);
                return CustomResponse();
            }
            catch (Exception e)
            {
                NotificarErro($"Exceção: {e.Message}.");
                return CustomResponse();
            }
        }

        [HttpGet(Name = "ListagemUsuarios")]
        public ActionResult ListagemUsuarios()
        {
            var users = _userManager.Users;

            return CustomResponse(users);
        }

        [HttpGet(Name = "EAdmin")]
        public async Task<ActionResult> EAdmin()
        {
            var userIdentity = await _userManager.FindByIdAsync(UsuarioId.ToString());

            if (userIdentity == null)
                return SendBadRequest($"O usuário não foi encontrado.");
            
            var usuario = await _usuarioService.ObterPorIdIdentity(userIdentity.Id);

            if (usuario == null)
                return SendBadRequest($"O usuário {userIdentity.Id} não foi encontrado.");

            if (!await _usuarioService.EAdmin(usuario.Id))
                return SendBadRequest("Não tem permissão.");

            return CustomResponse();
        }


        private async Task<LoginResponseViewModel> GerarJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandle = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandle.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandle.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
