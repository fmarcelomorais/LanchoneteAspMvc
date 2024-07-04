using LanchoneteAspMvc.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchoneteAspMvc.Models
{
    public class Carrinho : Entidade
    {
        public string CarrinhoId { get; set; }
        public List<Item> Itens { get; set; }

        private readonly LanchoneteContext _context;
        public Carrinho(LanchoneteContext context)
        {
            _context = context;
        }

        public static Carrinho RetornaCarrinho(IServiceProvider service)
        {

            ISession sess = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<LanchoneteContext>();

            string carrinhoIdSession = sess.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            sess.SetString("CarrinhoId", carrinhoIdSession);

            return new Carrinho(context)
            {
                CarrinhoId = carrinhoIdSession
            };
        }

        public void AdicionaItem(Lanche lanche)
        {

            var item = _context.Itens.SingleOrDefault(
                i => i.Lanche.Id == lanche.Id && i.CarrinhoId == CarrinhoId);
            if (item == null)
            {
                item = new Item
                {
                    CarrinhoId = CarrinhoId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.Itens.Add(item);
            }
            else
            {
                item.Quantidade++;
            }
            _context.SaveChanges();
        }

        public void LimpaCarrinho()
        {
            var carrinho = _context.Itens.Where(c => c.CarrinhoId == CarrinhoId);
            _context.Itens.RemoveRange(carrinho);
            _context.SaveChanges();
        }

        public void RemoveItem(Lanche lanche)
        {
            var item = _context.Itens.SingleOrDefault(
                i => i.Lanche.Id == lanche.Id && i.CarrinhoId == CarrinhoId);

            if (item != null)
            {
                if (item.Quantidade > 1)
                {
                    item.Quantidade--;
                }
                else
                {
                    _context.Itens.Remove(item);
                }
                _context.SaveChanges();
            }
        }

        public List<Item> RetornaItemCarrinhoCompra()
        {
            List<Item> itens = new List<Item>();

            itens = _context.Itens.Include(c => c.Lanche).Where(c => c.CarrinhoId == CarrinhoId).ToList();
            return itens;


        }

        public decimal RetornaTotalCarrinho()
        {
            var total = _context.Itens
                .Where(c => c.CarrinhoId == CarrinhoId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total;
        }
    }
}
