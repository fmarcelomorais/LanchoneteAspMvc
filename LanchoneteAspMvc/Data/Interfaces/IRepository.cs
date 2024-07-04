using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LanchoneteAspMvc.Data.Interfaces
{
    public interface IRepository<T> where T : Entidade
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task<bool> Post(T entidade);
        Task<bool> Put(T entidade);
        Task<bool> Delete(Guid id);
        Task<List<T>> Buscar(Expression<Func<T, bool>> expression);
        List<T> ReturaLista();
        LanchoneteContext RetornaContext();

    }
}
