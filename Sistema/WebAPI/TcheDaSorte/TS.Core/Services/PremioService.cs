using TS.Core.Interfaces;
using TS.Model.ViewModels;
using TS.Data.Interfaces;
using TS.Models.Models.Validations;
using AutoMapper;
using TS.Model.Models;
using TS.Model.Interfaces;

namespace TS.Core.Services
{
    public class PremioService : BaseService, IPremioService
    {
        private readonly IPremioRepository _PremioRepository;
        private readonly IMapper _mapper;

        public PremioService(IPremioRepository PremioRepository,
                              IMapper mapper,
                              INotificador notificador) : base(notificador)
        {
            _PremioRepository = PremioRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(Premio Premio)
        {
            if (!ExecutarValidacao(new PremioValidation(this), Premio)) return;

            await _PremioRepository.Adicionar(Premio);
        }

        public async Task Atualizar(Premio Premio)
        {
            if (!ExecutarValidacao(new PremioValidation(this), Premio)) return;

            await _PremioRepository.Atualizar(Premio);
        }

        public async Task Remover(int id)
        {
            var obj = await _PremioRepository.ObterPorId(id);
            await _PremioRepository.Remover(obj);
        }

        public bool CodigoExistente(string codigo)
        {
            return _PremioRepository.CodigoExistente(codigo);
        }

        public void Dispose()
        {
            _PremioRepository?.Dispose();
        }

        public async Task<List<PremioViewModel>> ObterPremiosDisponiveisAsNoTracking()
        {
            return _mapper.Map<List<PremioViewModel>>(await _PremioRepository.ObterPremiosDisponiveisAsNoTracking());
        }

        public async Task<PremioViewModel> ObterPremioECartelasAsNoTracking(int idPremio)
        {
            return _mapper.Map<PremioViewModel>(await _PremioRepository.ObterPremioECartelasAsNoTracking(idPremio));
        }
    }
}