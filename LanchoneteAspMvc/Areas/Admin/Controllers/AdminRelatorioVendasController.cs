using LanchoneteAspMvc.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasServices _relatorioVendasServices;

        public AdminRelatorioVendasController(RelatorioVendasServices relatorioVendasServices)
        {
            _relatorioVendasServices = relatorioVendasServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendaSimples(DateTime? minDate, DateTime? maxDate) 
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            } 
            if(!maxDate.HasValue)
            {
                maxDate = new DateTime(DateTime.Now.Year, 12, 30);
            }
       
            ViewBag.MinDate = minDate.Value.ToString("yyyy-MM-dd"); ;
            ViewBag.MaxDate = maxDate.Value.ToString("yyyy-MM-dd"); ;

            var result = await _relatorioVendasServices.FindByDate(minDate, maxDate);

            return View(result);
        }
    }
}
