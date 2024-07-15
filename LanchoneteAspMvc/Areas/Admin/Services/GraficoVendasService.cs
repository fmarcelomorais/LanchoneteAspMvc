using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;

namespace LanchoneteAspMvc.Areas.Admin.Services
{
    public class GraficoVendasService
    {
        private readonly IPedidoRepository _services;

        public GraficoVendasService(IPedidoRepository services)
        {
            _services = services;
        }

        public List<LancheGrafico> ObterVendaLanches(int dias = 360)
        {
            var context = _services.RetornaContext();

            var data = DateTime.Now.AddDays(-dias);

            var lanches = (from pedido in context.PedidoDetalhes
                           join lanche in context.Lanches on pedido.LancheId equals lanche.Id
                           where pedido.Pedido.PedidoEnviado >= data
                           group pedido by new { pedido.LancheId, lanche.Nome, pedido.Quantidade }
                           into g
                           select new
                           {
                               Nome = g.Key.Nome,
                               Quantidade = g.Sum(q => q.Quantidade),
                               ValorTotal = g.Sum(t => t.Preco * t.Quantidade)
                           });

            var listaLancheGrafico = new List<LancheGrafico>();

            foreach (var item in lanches)
            {
                var lancheGrafico = new LancheGrafico()
                {
                    Nome = item.Nome,
                    Quantidade = item.Quantidade,
                    ValorTotal = item.ValorTotal,
                };

                listaLancheGrafico.Add(lancheGrafico);
            }

            return listaLancheGrafico;
        }

    }
}
