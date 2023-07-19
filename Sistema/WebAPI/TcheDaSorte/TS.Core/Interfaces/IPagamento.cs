using TS.Core.Notificacoes;
using TS.Model.Models;

namespace TS.Core.Interfaces
{
    public interface IPagamento
    {
        bool PublicarPagamento(Cartela cartela);
        bool ProcessarPagamento(int idUsuario, int idCartela);

    }
}