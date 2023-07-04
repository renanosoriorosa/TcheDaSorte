using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Models.Models;
using Microsoft.EntityFrameworkCore;
using TS.Model.Models;

namespace TS.Data.Repository
{
    public class PremioRepository : Repository<Premio>, IPremioRepository
    {
        public PremioRepository(TSContext context) : base(context) { }

        public async Task<Premio> ObterPorCodigo(string codigo)
        {
            return await _context.Premio
                .Where(obj => obj.Codigo == codigo)
                .FirstOrDefaultAsync();
        }
    }
}