using TS.Model.Models;
using TS.Model.ViewModels;

namespace TS.Model.Interfaces
{
    public interface ICartelaService : IDisposable
    {
        Task Adicionar(Cartela cartela);
        Task Atualizar(Cartela cartela);
        Task Remover(int id);
        Task RemoverTodasAsCartelasDoPremio(int idPremio);
        Task CriarCartelasParaPremio(int idPremio, int numeroCartelas, double preco);
        Cartela GerarCartelaRandomica(int idPremio, double preco);
        Task<List<CartelaViewModel>> ObterTodosPorPremioId(int idPremio);
        Task<CartelaViewModel> ObterPorId(int idCartela);
        Task<Cartela> ObterCartelaPorId(int idCartela);
        Task<List<CartelaViewModel>> ObterTodosPorPremioIdUsuario(int idUsuario);
        Task<CartelaViewModel> ObterTodosOsDadosPorId(int idCartela);
    }
}