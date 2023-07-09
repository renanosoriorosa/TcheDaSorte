using TS.Model.Models;

namespace TS.Core.Interfaces
{
    public interface ICartelaService : IDisposable
    {
        Task Adicionar(Cartela cartela);
        Task Atualizar(Cartela cartela);
        Task Remover(int id);
        Task RemoverTodasAsCartelasDoPremio(int idPremio);
        Task CriarCartelasParaPremio(int idPremio, int numeroCartelas, double preco);
        Cartela GerarCartelaRandomica(int idPremio, double preco);
    }
}