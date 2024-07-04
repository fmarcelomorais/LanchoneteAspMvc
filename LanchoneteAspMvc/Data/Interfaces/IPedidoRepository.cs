using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Models;

namespace LanchoneteAspMvc.Data.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        void CriarPedido(Pedido pedido);
    }
}
