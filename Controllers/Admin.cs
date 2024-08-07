using Microsoft.AspNetCore.Mvc;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;

namespace WIRKDEVELOPER.Controllers
{
	public class Admin : Controller
	{
        private readonly ApplicationDBContext _Context;
         
        public Admin (ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult AdminDashboard()
		{
			return View();
		}
        public IActionResult IndexDosageForm()
        {
            IEnumerable<DosageForm> objList = _Context.dosageForms;
            return View(objList);

        }
        public IActionResult DosageForm()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DosageForm(DosageForm dosageForm)
        {
            if (ModelState.IsValid)
            {
                _Context.dosageForms.Update(dosageForm);
                _Context.SaveChanges();
                return RedirectToAction("IndexDosageForm");
            }
            return View(dosageForm);

        }
        public IActionResult UpdateDosageForm(int? DosageFormID)
        {
            if (DosageFormID == null || DosageFormID == 0)
            {
                return NotFound();
            }
            var obj = _Context.dosageForms.Find(DosageFormID);

            if (DosageFormID == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDosageForm(DosageForm dosageForm)
        {
            if (ModelState.IsValid)
            {
                _Context.dosageForms.Update(dosageForm);
                _Context.SaveChanges();
                return RedirectToAction("IndexDosageForm");
            }
            return View(dosageForm);
        }
        public IActionResult DeleteDosageForm(int? DosageFormID)
        {
            var obj = _Context.dosageForms.Find(DosageFormID);

            if (obj == null)
            {
                return NotFound();
            }
            _Context.dosageForms.Remove(obj);
            _Context.SaveChanges();
            return RedirectToAction("IndexDosageForm");
        }
    }
}
