using TS.Model.Models;
using TS.ViewModels.ViewModels;

namespace TS.Core.Interfaces
{
    public interface IPremioService : IDisposable
    {
        Task Adicionar(Premio premio);
        Task Atualizar(Premio premio);
        Task Remover(int id);
        Task<bool> CodigoExistente(string codigo);
        Task<List<PremioViewModel>> ObterPremiosDisponiveisAsNoTracking();
    }
}