using Microsoft.AspNetCore.Mvc;

namespace PF.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
