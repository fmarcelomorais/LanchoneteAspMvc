namespace LanchoneteAspMvc.Models
{
    public class Item : Entidade
    {
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }
        public string CarrinhoId { get; set; }
    }
}
