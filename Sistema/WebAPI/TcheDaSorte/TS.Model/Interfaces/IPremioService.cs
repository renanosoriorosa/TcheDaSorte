using TS.Model.Models;
using TS.Model.ViewModels;

namespace TS.Model.Interfaces
{
    public interface IPremioService : IDisposable
    {
        Task Adicionar(Premio premio);
        Task Atualizar(Premio premio);
        Task Remover(int id);
        bool CodigoExistente(string codigo);
        Task<List<PremioViewModel>> ObterPremiosDisponiveisAsNoTracking();
        Task<PremioViewModel> ObterPremioECartelasAsNoTracking(int idPremio);
    }
}