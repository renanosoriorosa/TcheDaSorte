using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Models.Models;
using Microsoft.EntityFrameworkCore;
using TS.Model.Models;
using GP.Models.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<Premio>> ObterPremiosDisponiveisAsNoTracking(int pagina, int tamanhoPagina)
        {
            return await _context.Premio
                .Where(obj => obj.Status == PremioStatusEnum.Criado || 
                obj.Status == PremioStatusEnum.Acumulado)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
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