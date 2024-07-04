namespace LanchoneteAspMvc.Models
{
    public class Pedido : Entidade
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public decimal PedidoTotal { get; set; }
        public int TotalItensPedido { get; set; }
        public DateTime PedidoEnviado { get; set; }
        public DateTime? PedidoEntregueEm { get; set; }

        public List<PedidoDetalhe>? PedidoDetalhes { get; set; }


    }
}
