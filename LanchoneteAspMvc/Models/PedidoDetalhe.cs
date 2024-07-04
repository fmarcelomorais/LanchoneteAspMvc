namespace LanchoneteAspMvc.Models
{
    public class PedidoDetalhe : Entidade
    {
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public Guid LancheId {  get; set; }
        public Guid PedidoId { get; set; }

        public Lanche Lanche { get; set; }
        public Pedido Pedido { get; set; }
    }
}
