using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.API.Interfaces;
using TS.Core.Interfaces;
using TS.Model.Interfaces;

namespace TS.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("v{version:apiVersion}/[controller]/[action]")]
    public class SorteioController : MainController
    {
        private readonly ICartelaService _cartelaService;
        private readonly IPremioService _premioService;

        public SorteioController(INotificador notificador,
            IUser user,
            IPremioService premioService,
            ICartelaService cartelaService) : base(notificador, user)
        {
            _cartelaService = cartelaService;
            _premioService = premioService;
        }

        [HttpPost("idPremio:int")]
        public async Task<ActionResult> Sortear(int idPremio)
        {
            return CustomResponse(await _premioService.SortearCartela(idPremio));
        }
    }
}
