using TS.Data.Context;
using TS.Data.Interfaces;
using TS.Models.Models;
using Microsoft.EntityFrameworkCore;
using TS.Model.Models;
using TS.Model.ViewModels;

namespace TS.Data.Repository
{
    public class CartelaRepository : Repository<Cartela>, ICartelaRepository
    {
        public CartelaRepository(TSContext context) : base(context) { }

        public async Task<Cartela> ObterPorPremioId(int idPremio)
        {
            return await _context.Cartela
                .Where(obj => obj.PremioId == idPremio)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Cartela>> ObterTodosPorPremioId(int idPremio)
        {
            return await _context.Cartela
                .Select(c => new Cartela
                (
                    c.Id,
                    c.Codigo,
                    c.PremioId,
                    c.UsuarioId,
                    c.Preco,
                    c.Sorteada,
                    c.CompraAprovada,
                    c.PrimeiroNumero,
                    c.SegundoNumero,
                    c.TerceiroNumero,
                    c.QuartoNumero,
                    c.QuintoNumero
                ))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Cartela>> ObterTodosPorPremioIdUsuario(int idUsuario)
        {
            return await _context.Cartela
                .Where(x => x.UsuarioId == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cartela> ObterTodosOsDadosPorId(int idCartela)
        {
            return await _context.Cartela
                .Where(x => x.Id == idCartela)
                .Include(x => x.Premio)
                .Include(x => x.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}