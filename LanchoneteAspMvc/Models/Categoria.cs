namespace LanchoneteAspMvc.Models
{
    public class Categoria : Entidade
    {
        public string Nome { get;  set; }
        public string Descricao { get;  set; }
        public List<Lanche>? Lanches { get;  set; }
    }

}
