using LanchoneteAspMvc.Models;
using System.Linq.Expressions;

namespace LanchoneteAspMvc.Data.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        List<Categoria> RetornaCategoria();
        public bool IfExists(Expression<Func<Categoria, bool>> expression);
    }
}
