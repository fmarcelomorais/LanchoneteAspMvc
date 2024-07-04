using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LanchoneteAspMvc.Areas.Admin.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly LanchoneteContext _context;
        public CategoriaRepository(LanchoneteContext context) { _context = context; }

        public async Task<List<Categoria>> GetAll()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }
        public async Task<Categoria> Get(Guid id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(cat => cat.Id == id);
        }
        public async Task<bool> Post(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            return await _context.SaveChangesAsync() == 1 ? true : false;

        }
        public async Task<bool> Delete(Guid id)
        {
            var categoria = await Get(id);
            _context.Categorias.Remove(categoria);
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }
        public async Task<bool> Put(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public List<Categoria> RetornaCategoria()
        {
            var categorias = _context.Categorias.OrderBy(c => c.Nome).ToList();
            return categorias;
        }

        public async Task<List<Categoria>> Buscar(Expression<Func<Categoria, bool>> expression)
        {
            var categorias = await _context.Categorias.Where(expression).ToListAsync();
            return categorias;
        }

        public bool IfExists(Expression<Func<Categoria, bool>> expression)
        {
            return _context.Categorias.Any(expression);
          
        }

        public List<Categoria> ReturaLista()
        {
            return _context.Categorias.ToList();
        }

        public LanchoneteContext RetornaContext()
        {
            return _context;
        }
    }
}
