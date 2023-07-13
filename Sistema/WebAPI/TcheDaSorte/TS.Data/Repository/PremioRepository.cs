using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Models.Models;
using Microsoft.EntityFrameworkCore;
using TS.Model.Models;
using GP.Models.Enums;

namespace TS.Data.Repository
{
    public class PremioRepository : Repository<Premio>, IPremioRepository
    {
        public PremioRepository(TSContext context) : base(context) { }

        public bool CodigoExistente(string codigo)
        {
            return _context.Premio
                .Any(obj => obj.Codigo == codigo);
        }

        public async Task<Premio> ObterPorCodigo(string codigo)
        {
            return await _context.Premio
                .Where(obj => obj.Codigo == codigo)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Premio>> ObterPremiosDisponiveisAsNoTracking()
        {
            return await _context.Premio
                .Where(obj => obj.Status == PremioStatusEnum.Criado || 
                obj.Status == PremioStatusEnum.Acumulado)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Premio> ObterPremioECartelasAsNoTracking(int idPremio)
        {
            return await _context.Premio
                .Where(obj => obj.Id == idPremio)
                .Include(obj => obj.Cartelas)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}