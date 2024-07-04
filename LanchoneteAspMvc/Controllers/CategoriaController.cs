using Microsoft.AspNetCore.Mvc;

namespace LanchoneteAspMvc.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
