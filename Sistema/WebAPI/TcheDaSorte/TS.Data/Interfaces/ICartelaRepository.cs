using TS.Model.Models;
using TS.Models.Models;

namespace TS.Data.Interfaces
{
    public interface ICartelaRepository : IRepository<Cartela>
    {
        Task<Cartela> ObterPorPremioId(int idPremio);
    }
}