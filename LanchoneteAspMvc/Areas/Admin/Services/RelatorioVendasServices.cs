using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchoneteAspMvc.Areas.Admin.Services
{
    public class RelatorioVendasServices
    {
        private readonly IPedidoRepository _pedidoRepository;

        public RelatorioVendasServices(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<Pedido>> FindByDate(DateTime? min, DateTime? max)
        {
            var context = _pedidoRepository.RetornaContext();
            var result = from obj in context.Pedidos select obj;
            if(min.HasValue)
            {
                result = result.Where(x => x.PedidoEnviado >= min.Value);
            }
            if(max.HasValue)
            {
                result = result.Where(x => x.PedidoEnviado <= max.Value);
            }

            return await result
                .Include(l => l.PedidoDetalhes)
                .ThenInclude(l => l.Lanche)
                .OrderByDescending(x => x.PedidoEnviado)
                .ToListAsync();
        }
    }
}
