using TS.Model.Models;
using TS.Models.Models;

namespace TS.Data.Interfaces
{
    public interface ICartelaRepository : IRepository<Cartela>
    {
        Task<Cartela> ObterPorPremioId(int idPremio);
        Task<List<Cartela>> ObterTodosPorPremioId(int idPremio);
        Task<Cartela> ObterTodosOsDadosPorId(int idCartela);
        Task<List<Cartela>> ObterTodosPorIdUsuario(int idUsuario,
            int pagina, int tamanhoPagina);
        Task<List<Cartela>> ObterTodosDisponiveisPraSorteio(int idPremio);
    }
}