using LanchoneteAspMvc.Models;

namespace LanchoneteAspMvc.ViewModels
{
    public class PedidoLancheVM
    {
        public Pedido Pedido { get; set; }
        public List<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
