using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using WIRKDEVELOPER.Models;
using System.Security.Claims;

namespace WIRKDEVELOPER.Controllers
{
	public class AnaesthesiologistController : Controller
	{
		private readonly ApplicationDBContext _Context;

		public AnaesthesiologistController(ApplicationDBContext applicationDBContext)
		{
			_Context = applicationDBContext;
		}
		public IActionResult Anaesthesiologist()
		{
			return View();
		}
		public IActionResult IndexViewPatient(DateTime searchDate)
		{
			//IEnumerable<ViewPatient> objList = _Context.ViewRecords;
			//return View(objList);
			var patient = _Context.viewrecords.Where(e => e.Date == searchDate.Date).ToList();
			return View(patient);
		}
		public IActionResult ViewPatient()
		{
			return View();
		}
		public IActionResult ViewPatientRec()
		{
			return View();
		}
		public IActionResult IndexAnVitals()
		{
			IEnumerable<AnVitals> objList = _Context.anvitals;
			return View(objList);
			//var patient = _Context.ViewRecords.Where(e => e.Date == searchDate.Date).ToList();
			//return View(patient);
		}
		public IActionResult AnVitals()
		{
			return View();
		}
		public IActionResult IndexAnAllergies()
		{
			IEnumerable<AnAllergies> objList = _Context.anallergies;
			return View(objList);

		}
		public IActionResult AnAllergies()
		{
			return View();
		}
		public IActionResult IndexCurrentMedication()
		{
			IEnumerable<AnCurrentMedication> objList = _Context.ancurrentmedication;
			return View(objList);

		}
		public IActionResult AnCurrentMedication()
		{
			return View();
		}
		public IActionResult IndexConditions()
		{
			IEnumerable<AnConditions> objList = _Context.anconditions;
			return View(objList);

		}
		public IActionResult AnConditions()
		{
			return View();
		}
		public IActionResult IndexOrder()
		{
			IEnumerable<Order> objList = _Context.order;
			return View(objList);

		}
		public IActionResult Order()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Order(Order order)
		{
			var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
			order.Patient = user;

			if (ModelState.IsValid)
			{

				_Context.order.Add(order);
				_Context.SaveChanges();
				return RedirectToAction("IndexOrder");
			}
			ViewData["AdmissionID"] = new SelectList(_Context.Users, "Id", "Id", order.Patient);
			return View(order);

		}
		public IActionResult NewUpdateOrder(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			var obj = _Context.order.Find(ID);

			if (ID == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult NewUpdateOrder(Order order)
		{
			if (ModelState.IsValid)
			{
				_Context.order.Update(order);
				_Context.SaveChanges();
				return RedirectToAction("IndexOrder");
			}
			return View(order);
		}
		public IActionResult DeleteOrder(int? ID)
		{
			var obj = _Context.order.Find(ID);

			if (obj == null)
			{
				return NotFound();
			}
			_Context.order.Remove(obj);
			_Context.SaveChanges();
			return RedirectToAction("IndexOrder");
		}
		public IActionResult IndexVitalRanges()
		{
			IEnumerable<VitalRanges> objList = _Context.vitalranges;
			return View(objList);

		}
		public IActionResult VitalRanges()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VitalRanges(VitalRanges vitalranges)
        {
            if (ModelState.IsValid)
            {
                _Context.vitalranges.Update(vitalranges);
                _Context.SaveChanges();
                return RedirectToAction("IndexVitalRanges");
            }
            return View(vitalranges);

        }
        public IActionResult UpdateVitalRanges(int? VitalRangeID)
        {
            if (VitalRangeID == null || VitalRangeID == 0)
            {
                return NotFound();
            }
            var obj = _Context.vitalranges.Find(VitalRangeID);

            if (VitalRangeID == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateVitalRanges(VitalRanges vitalRanges)
        {
            if (ModelState.IsValid)
            {
                _Context.vitalranges.Update(vitalRanges);
                _Context.SaveChanges();
                return RedirectToAction("IndexVitalRanges");
            }
            return View(vitalRanges);
        }
        public IActionResult DeleteVitalRanges(int? VitalRangeID)
        {
            var obj = _Context.vitalranges.Find(VitalRangeID);

            if (obj == null)
            {
                return NotFound();
            }
            _Context.vitalranges.Remove(obj);
            _Context.SaveChanges();
            return RedirectToAction("IndexVitalRanges");
        }
        public IActionResult IndexNotes(string searchPatient)
		{
			//IEnumerable<Notes> objList = _Context.notes;
			//return View(objList);
			//var patient = _Context.notes.Where(e => e.Patient == searchPatient.Patient).ToList();
			//return View(patient);

			var patients = _Context.notes.Select(p => new SelectListItem
			{
				Value = p.NotesID.ToString(),
				Text = p.Patient
			}).ToList();

			ViewBag.PatientList = patients;
			return View();

		}
		//public IActionResult Notes()
		//{
		//    return View();
		//}
		//[HttpPost]
		public IActionResult Notes(int patientId)
		{
			var patient = _Context.notes.FirstOrDefault(p => p.NotesID == patientId);
			return View(patient);
		}
        public IActionResult IndexVitalHistory()
        {
            IEnumerable<AnVitals> objList = _Context.anvitals;
            return View(objList);
        }
        public IActionResult VitalHistory()
        {
            return View();
        }

    }
}
