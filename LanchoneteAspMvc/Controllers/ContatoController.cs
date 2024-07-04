using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
