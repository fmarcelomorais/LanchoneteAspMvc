using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Areas.Admin.Repositories ;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoryRepository;

        public CategoriaMenu(ICategoriaRepository repository)
        {
            _categoryRepository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoryRepository.RetornaCategoria();

            return View(categorias);
        }
    }
}
