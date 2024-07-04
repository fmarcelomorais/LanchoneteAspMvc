using LanchoneteAspMvc.Data.Context;
using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LanchoneteAspMvc.Areas.Admin.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly LanchoneteContext _context;
        private readonly Carrinho _carrinho;

        public PedidoRepository(LanchoneteContext context, Carrinho carrinho)
        {
            _context = context;
            _carrinho = carrinho;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var carrinho = _carrinho.Itens;
            foreach (var item in carrinho)
            {
                var detalhe = new PedidoDetalhe()
                {
                    LancheId = item.Lanche.Id,
                    PedidoId = pedido.Id,
                    Preco = item.Lanche.Preco,
                    Quantidade = item.Quantidade,
                };
                _context.PedidoDetalhes.Add(detalhe);
            }
            _context.SaveChanges();
        }

        public async Task<bool> Delete(Guid id)
        {
            var pedido = await Get(id);
            _context.Pedidos.Remove(pedido);
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<Pedido> Get(Guid id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
                
        }

        public async Task<List<Pedido>> GetAll()
        {
            return await _context.Pedidos.AsNoTracking().ToListAsync();
        }

        public Task<bool> Post(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Put(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<List<Pedido>> Buscar(Expression<Func<Pedido, bool>> expression)
        {
            return await _context.Pedidos.Where(expression).ToListAsync();
        }

        public List<Pedido> ReturaLista()
        {
            return _context.Pedidos.ToList();
        }

        public LanchoneteContext RetornaContext()
        {
            return _context;
        }
    }
}
