using LanchoneteAspMvc.Data.Interfaces;
using LanchoneteAspMvc.Models;
using LanchoneteAspMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LanchoneteAspMvc.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILancheRepository _lancheRepository;
        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lanchesPreferidos = await _lancheRepository.LanchesPreferidos();
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = lanchesPreferidos
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
