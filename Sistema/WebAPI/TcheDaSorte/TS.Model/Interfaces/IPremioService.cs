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
        Task<ResponsePaginacaoViewModel<PremioViewModel>> ObterPremiosDisponiveisAsNoTracking(
            int pagina,
            int tamanhoPagina);
        Task<PremioViewModel> ObterPremioECartelasAsNoTracking(int idPremio);
        Task<PremioViewModel> ObterPorIdAsNoTracking(int idPremio);
        Task<CartelaViewModel> SortearCartela(int idPremio);
    }
}