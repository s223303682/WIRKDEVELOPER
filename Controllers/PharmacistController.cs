using Microsoft.AspNetCore.Mvc;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;

namespace WIRKDEVELOPER.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly ApplicationDBContext _Context;

        public PharmacistController(ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult Createmedication()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult CreateMedication(Medication medication)
        {
            if (ModelState.IsValid)
            {
                _Context.medications.Add(medication);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(medication);
        }
        public IActionResult Createstock()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Createstock(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _Context.stocks.Add(stock);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(stock);
        }
    }
}
