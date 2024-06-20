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
		public IActionResult AnOrder()
		{
			return View();
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
	}
}
