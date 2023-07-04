using TS.Model.Models;
using TS.Models.Models;

namespace TS.Data.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterUsuarioPorIdIdentity(Guid tipo);
    }
}