using TS.Model.Models;
using TS.ViewModels.ViewModels;

namespace TS.Core.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(Usuario produto);
        Task Atualizar(Usuario produto);
        Task Remover(int id);
        Task RemoverPorIdIdentity(string idIdentity);
        Task<UsuarioViewModel> ObterPorId(int id);
        Task<List<UsuarioViewModel>> ObterTodos();
    }
}