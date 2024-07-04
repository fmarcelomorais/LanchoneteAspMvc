using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LanchoneteAspMvc.Areas.Admin.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly LanchoneteContext _context;
        public LancheRepository(LanchoneteContext context) { _context = context; }
        public async Task<List<Lanche>> GetAll()
        {
            var lanches = await _context.Lanches
                .AsNoTracking()
                .Include(cat => cat.Categoria)
                .ToListAsync();
            return lanches;
        }
        public async Task<Lanche> Get(Guid id)
        {
            return await _context.Lanches.FirstOrDefaultAsync(lan => lan.Id == id);
        }
        public async Task<bool> Post(Lanche lanche)
        {
            await _context.Lanches.AddAsync(lanche);
            return await _context.SaveChangesAsync() == 1 ? true : false;

        }
        public async Task<bool> Delete(Guid id)
        {
            var lanche = await Get(id);
            _context.Lanches.Remove(lanche);
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }
        public async Task<bool> Put(Lanche lanche)
        {
            _context.Entry(lanche).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<List<Lanche>> LanchesPreferidos()
        {
            return await _context.Lanches
                .OrderBy(l => l.Disponivel == true)
                .Where(lanche => lanche.Preferido == true)
                .Include(cat => cat.Categoria)
                .ToListAsync();
        }

        public async Task<List<Lanche>> LanchesPorCategoria(string categoria)
        {
            return await _context.Lanches
                .Include(c => c.Categoria)
                .Where(l => l.Categoria.Nome.Equals(categoria))
                .OrderBy(l => l.Nome)
                .ToListAsync();
        }

        public async Task<List<Lanche>> Buscar(Expression<Func<Lanche, bool>> expression)
        {
            return await _context.Lanches.Where(expression).ToListAsync();
        }

        public bool IfExists(Expression<Func<Lanche, bool>> expression)
        {
            return _context.Lanches.Any(expression);

        }

        public List<Lanche> ReturaLista()
        {
            return _context.Lanches.Include(c => c.Categoria).ToList();
        }

        public LanchoneteContext RetornaContext()
        {
            return _context;
        }
    }
}
