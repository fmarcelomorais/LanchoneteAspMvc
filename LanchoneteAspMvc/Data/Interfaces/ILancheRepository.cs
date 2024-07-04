using LanchoneteAspMvc.Models;
using System.Linq.Expressions;

namespace LanchoneteAspMvc.Data.Interfaces
{
    public interface ILancheRepository : IRepository<Lanche>
    {
        Task<List<Lanche>> LanchesPreferidos();
        Task<List<Lanche>> LanchesPorCategoria(string categoria);
        Task<List<Lanche>> Buscar(Expression<Func<Lanche, bool>> expression);
        bool IfExists(Expression<Func<Lanche, bool>> expression);
    }
}
