using Microsoft.AspNetCore.Mvc;

namespace WIRKDEVELOPER.Controllers
{
    public class NurseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
