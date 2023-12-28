using TS.Model.Models;

namespace TS.Core.Interfaces
{
    public interface IPagamentoService
    {
        bool PublicarPagamento(Cartela cartela);
        Task ProcessarPagamentos();

    }
}