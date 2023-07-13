using TS.Core.Interfaces;
using TS.Model.ViewModels;
using TS.Data.Interfaces;
using TS.Models.Models.Validations;
using AutoMapper;
using TS.Model.Models;
using TS.Model.Interfaces;

namespace TS.Core.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository UsuarioRepository,
                              IMapper mapper,
                              INotificador notificador) : base(notificador)
        {
            _UsuarioRepository = UsuarioRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(Usuario Usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), Usuario)) return;

            await _UsuarioRepository.Adicionar(Usuario);
        }

        public async Task Atualizar(Usuario Usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), Usuario)) return;

            await _UsuarioRepository.Atualizar(Usuario);
        }

        public async Task Remover(int id)
        {
            var obj = await _UsuarioRepository.ObterPorId(id);
            await _UsuarioRepository.Remover(obj);
        }

        public async Task RemoverPorIdIdentity(string idIdentity)
        {
            var obj = await _UsuarioRepository.ObterUsuarioPorIdIdentity(idIdentity);
            await _UsuarioRepository.Remover(obj);
        }

        public async Task<UsuarioViewModel> ObterPorIdIdentity(string idIdentity)
        {
            return _mapper.Map<UsuarioViewModel>(await _UsuarioRepository.ObterUsuarioPorIdIdentity(idIdentity));
        }

        public async Task<UsuarioViewModel> ObterPorId(int id)
        {
            return _mapper.Map<UsuarioViewModel>(await _UsuarioRepository.ObterPorId(id));
        }

        public async Task<List<UsuarioViewModel>> ObterTodos()
        {
            return _mapper.Map<List<UsuarioViewModel>>(await _UsuarioRepository.ObterTodos());
        }

        public void Dispose()
        {
            _UsuarioRepository?.Dispose();
        }
    }
}