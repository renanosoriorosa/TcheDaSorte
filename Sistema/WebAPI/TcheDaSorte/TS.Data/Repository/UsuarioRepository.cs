using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Models.Models;
using Microsoft.EntityFrameworkCore;
using TS.Model.Models;

namespace TS.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TSContext context) : base(context) { }

        public async Task<Usuario> ObterUsuarioPorIdIdentity(Guid idIdentity)
        {
            return await _context.Usuario
                .Where(obj => obj.IndentityId == idIdentity)
                .FirstOrDefaultAsync();
        }
    }
}