using TS.Model.Models;
using TS.Model.ViewModels;

namespace TS.Core.Interfaces
{
    public interface IPagamentoService
    {
        bool PublicarPagamento(Cartela cartela);
        Task ProcessarPagamento(CartelaCompraViewModel cartelaFila);

    }
}