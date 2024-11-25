using Microsoft.AspNetCore.Mvc;

namespace PF.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
