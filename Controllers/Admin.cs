using Microsoft.AspNetCore.Mvc;

namespace WIRKDEVELOPER.Controllers
{
	public class Admin : Controller
	{
		public IActionResult AdminDashboard()
		{
			return View();
		}
	}
}
