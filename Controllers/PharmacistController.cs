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
        public IActionResult Pharmacy()
        {
            return View();
        }
        public IActionResult Createmedication()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMedication(Medication medication)
        {
            if (ModelState.IsValid)
            {
                _Context.medications.Add(medication);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(medication);
        }
        public IActionResult MedicationList()
        {
            IEnumerable<Medication> list = _Context.medications;
            return View(list);
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
        public IActionResult StockList()
        {
            IEnumerable<Stock> list = _Context.stocks;
            return View(list);
        }
        public IActionResult updateStock(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.stocks.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateStock( Stock stock)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.stocks.Update(stock);
            _Context.SaveChanges();
            return RedirectToAction("StockList");
        }
    }
}
