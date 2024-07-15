using LanchoneteAspMvc.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService _graficoVendas;

        public AdminGraficoController(GraficoVendasService graficoVendasService)
        {
            _graficoVendas = graficoVendasService;
        }

        [HttpGet]
        public IActionResult Index(int dias)
        {
            return View();
        } 
        
        [HttpGet]
        public IActionResult VendasSemanal(int dias)
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasMensal(int dias)
        {
            return View();
        }

        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendas = _graficoVendas.ObterVendaLanches(dias);

            return Json(lanchesVendas);
        }
    }
}
