using Microsoft.AspNetCore.Mvc;

namespace PF.Controllers
{
	public class Kanban : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
