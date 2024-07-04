namespace LanchoneteAspMvc.Models
{
    public class Lanche : Entidade
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLonga { get; set; }
        public string ImagemUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool Preferido { get; set; }
        public bool Disponivel { get; set; }

        /*EF Core*/
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

    }
}
