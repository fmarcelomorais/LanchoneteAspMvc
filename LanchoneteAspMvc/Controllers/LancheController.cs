using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using LanchoneteAspMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LanchoneteAspMvc.Controllers
{
    [Authorize]
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Catalogo(string categoria)
        {
            
            List<Lanche> lanches;
            string categoriaAtual = "";

            if(string.IsNullOrEmpty(categoria))
            {
                lanches = await _lancheRepository.GetAll();
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = await _lancheRepository.LanchesPorCategoria(categoria) ;
                categoriaAtual = categoria;
            }

            var catalogo = new CatalogoViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            
            };

            return View(catalogo);
        }

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var lanche = await _lancheRepository.Get(id);

            return View(lanche);
        }

        public async Task<ViewResult> Buscar(string busca)
        {
            List<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(busca))
            {
                lanches = await _lancheRepository.GetAll();
            }
            else
            {
                lanches = await _lancheRepository.Buscar(l => l.Nome.ToLower().Contains(busca.ToLower())) ;
                if(lanches.Any()) 
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = string.Empty;
                }
            }
            var catalogo = new CatalogoViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View("~/Views/Lanche/Catalogo.cshtml", catalogo);

        }
    }
}
