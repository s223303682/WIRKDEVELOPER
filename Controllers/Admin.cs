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
                _Context.dosageForms.Add(dosageForm);
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
        public IActionResult OperatingTheatreList()
        {
            IEnumerable<OperationTheatre> list = _Context.operationTheatres;
            return View(list);
        }
        public IActionResult CreateOperatingTheatre()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOperatingTheatre(OperationTheatre operationTheatre)
        {
            if (ModelState.IsValid)
            {
                _Context.operationTheatres.Add(operationTheatre);
                _Context.SaveChanges();
                return RedirectToAction("OperatingTheatreList");
            }
            return View(operationTheatre);
        }
        public IActionResult updateOperatingTheatre(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.operationTheatres.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateOperatingTheatre(OperationTheatre operationTheatre)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.operationTheatres.Update(operationTheatre);
            _Context.SaveChanges();
            return RedirectToAction("OperatingTheatreList");
        }
        public IActionResult DeleteOperatingTheatre(int? ID)
        {
            var list = _Context.operationTheatres.Find(ID);
            if (list == null)
            {
                return NotFound();
            }
            _Context.operationTheatres.Remove(list);
            _Context.SaveChanges();
            return RedirectToAction("OperatingTheatreList");

        }
        public IActionResult DayHospitalList()
        {
            IEnumerable<DayHospital> list = _Context.dayHospitals;
            return View(list);
        }
        public IActionResult CreateDayHospital()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDayHospital(DayHospital dayHospital)
        {
            if (ModelState.IsValid)
            {
                _Context.dayHospitals.Add(dayHospital);
                _Context.SaveChanges();
                return RedirectToAction("DayHospitalList");
            }
            return View(dayHospital);
        }
        public IActionResult updateDayHospital(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.dayHospitals.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateDayHospital(DayHospital dayHospital)
        {

            _Context.dayHospitals.Update(dayHospital);
            _Context.SaveChanges();
            return RedirectToAction("DayHospitalList");
        }
        public IActionResult DeleteDayHospital(int? ID)
        {
            var list = _Context.operationTheatres.Find(ID);
            if (list == null)
            {
                return NotFound();
            }
            _Context.operationTheatres.Remove(list);
            _Context.SaveChanges();
            return RedirectToAction("DayHospitalList");

        }
        public IActionResult IndexActive()
        {
            IEnumerable<ActiveIngredient> objList = _Context.activeIngredients;
            return View(objList);

        }
        public IActionResult ActiveIngredients()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActiveIngredients(ActiveIngredient activeIngredient)
        {
            if (ModelState.IsValid)
            {
                _Context.activeIngredients.Add(activeIngredient);
                _Context.SaveChanges();
                return RedirectToAction("IndexActive");
            }
            return View(activeIngredient);

        }

    }
}
