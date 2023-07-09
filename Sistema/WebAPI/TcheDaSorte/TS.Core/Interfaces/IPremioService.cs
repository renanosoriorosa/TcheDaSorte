using TS.Model.Models;

namespace TS.Core.Interfaces
{
    public interface IPremioService : IDisposable
    {
        Task Adicionar(Premio premio);
        Task Atualizar(Premio premio);
        Task Remover(int id);
        Task<bool> CodigoExistente(string codigo);
    }
}