using LanchoneteAspMvc.Models;

namespace LanchoneteAspMvc.ViewModels;

public class CatalogoViewModel
{
    public List<Lanche> Lanches { get; set; }
    public string CategoriaAtual { get; set; }
}
