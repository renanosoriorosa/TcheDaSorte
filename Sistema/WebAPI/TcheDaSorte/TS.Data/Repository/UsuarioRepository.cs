using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Models.Models;
using Microsoft.EntityFrameworkCore;
using TS.Model.Models;
using System.Security.Principal;

namespace TS.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TSContext context) : base(context) { }

        public async Task<bool> EAdmin(int usuarioId)
        {
            return await _context.Usuario
                .AnyAsync(obj => obj.Id == usuarioId && obj.Admin == true);
        }

        public async Task<Usuario> ObterUsuarioPorIdIdentity(string idIdentity)
        {
            return await _context.Usuario
                .Where(obj => obj.IndentityId == idIdentity)
                .FirstOrDefaultAsync();
        }
    }
}