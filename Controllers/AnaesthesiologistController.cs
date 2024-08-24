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

            //var patient = _Context.admission.Where(e => e.Date == searchDate.Date).ToList();
            //return View(patient);
            var patient = _Context.viewrecords.Where(e => e.Date == searchDate.Date).ToList();
            return View(patient);
        }
        public IActionResult ViewPatientRec()
		{
			return View();
		}
        public IActionResult IndexViewBookedPatients()
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
			IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication);
			return View(objList);

		}
		public IActionResult Order()
		{
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Order(Order order)
		{

            _Context.order.Add(order);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();


            return RedirectToAction("IndexOrder");

            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //order.Patient = user;

            //if (ModelState.IsValid)
            //{

            //	_Context.order.Add(order);
            //	_Context.SaveChanges();
            //	return RedirectToAction("IndexOrder");
            //}
            //ViewData["AdmissionID"] = new SelectList(_Context.Users, "Id", "Id", order.Patient);
            //return View(order);

        }
		public IActionResult NewUpdateOrder(int? ID)
		{


            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.order.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);
            //if (ID == null || ID == 0)
            //{
            //	return NotFound();
            //}
            //var obj = _Context.order.Find(ID);

            //if (ID == null)
            //{
            //	return NotFound();
            //}
            //return View(obj);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult NewUpdateOrder(Order order)
		{
            _Context.order.Update(order);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            ViewBag.getAdmission = new SelectList(_Context.admission, "AdmissionID", "PatientName");
            _Context.SaveChanges();
            return RedirectToAction("IndexOrder");
            //if (ModelState.IsValid)
            //{
            //	_Context.order.Update(order);
            //	_Context.SaveChanges();
            //	return RedirectToAction("IndexOrder");
            //}
            //return View(order);
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
        public IActionResult UpdateVitalRanges(int? ID)
        {

            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.vitalranges.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

            //if (VitalRangeID == null || VitalRangeID == 0)
            //{
            //    return NotFound();
            //}
            //var obj = _Context.vitalranges.Find(VitalRangeID);

            //if (VitalRangeID == null)
            //{
            //    return NotFound();
            //}
            //return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateVitalRanges(int ID, VitalRanges vitalRanges)
        {


            _Context.vitalranges.Update(vitalRanges);
            _Context.SaveChanges();
            return RedirectToAction("IndexVitalRanges");

            //if (ModelState.IsValid)
            //{
            //    _Context.vitalranges.Update(vitalRanges);
            //    _Context.SaveChanges();
            //    return RedirectToAction("IndexVitalRanges");
            //}
            //return View(vitalRanges);
        }
        public IActionResult DeleteVitalRanges(int? ID)
        {

            var obj = _Context.vitalranges.Find(ID);

            if (obj == null)
            {
                return NotFound();
            }
            _Context.vitalranges.Remove(obj);
            _Context.SaveChanges();
            return RedirectToAction("IndexVitalRanges");

            //var obj = _Context.vitalranges.Find(VitalRangeID);

            //if (obj == null)
            //{
            //    return NotFound();
            //}
            //_Context.vitalranges.Remove(obj);
            //_Context.SaveChanges();
            //return RedirectToAction("IndexVitalRanges");
        }
        //public PatientController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public IActionResult SearchPatient(string patientName)
        //{


            //var order= _Context.order
            //                      .Where(o => o.Patient.Contains(patientName))
            //                      .ToList();


            //return View(order);
            //IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication).Include(a => a.Admission);
            //return View(objList);
            //IEnumerable<Order> objList = _Context.order;
            //return View(objList);
       // }
		//public IActionResult ListOrder(Order order)
		//{
  //          if (ModelState.IsValid)
  //          {
  //              _Context.order.Add(order);
  //              _Context.SaveChanges();
  //              return RedirectToAction("IndexOrder");
  //          }
  //          return View(order);
  //      }


        //public IActionResult CreateNoteForOrder(int orderId, string noteText)
        //{
        //    var order = _Context.order.Find(orderId);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var note = new Notes
        //    {
        //        AnOrderID = orderId,
        //        notes = noteText,
        //        Date = DateTime.Now
        //    };

        //    _Context.notes.Add(note);
        //    _Context.SaveChanges();

        //    return RedirectToAction("Search", new { patientName = order.Patient });
        //}
        public IActionResult CreateNoteForOrder(int orderId)
        {
            var order = _Context.order.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

           // ViewBag.Patient = order.AdmissionID;

            var note = new Notes
            {
                AnOrderID = orderId
            };

            return View(note);
        }

        // POST: Handle the form submission
        [HttpPost]
        public IActionResult CreateNoteForOrder(Notes note)
        {
            if (ModelState.IsValid)
            {
                note.Date = DateTime.Now;
                _Context.notes.Add(note);
                _Context.SaveChanges();

                var order = _Context.order.Find(note.AnOrderID);
               // return RedirectToAction("Search", new { patientName = order.AdmissionID });
            }

            return View(note);
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
        //public IActionResult Indexclassadd()
        //{
        //    IEnumerable<ClassAdd> objList = _Context.classadd;
        //    return View(objList);
        //}
        //public IActionResult ClassAdd()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ClassAdd(ClassAdd classAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _Context.classadd.Add(classAdd);
        //        _Context.SaveChanges();
        //        return RedirectToAction("Indexclassadd");
        //    }
        //    return View(classAdd);

        //}
        //public IActionResult Updateclassadd(int? AdmissionID)
        //{
        //    if (AdmissionID == null || AdmissionID == 0)
        //    {
        //        return NotFound();
        //    }
        //    var obj = _Context.classadd.Find(AdmissionID);

        //    if (AdmissionID == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Updateclassadd(ClassAdd classAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _Context.classadd.Update(classAdd);
        //        _Context.SaveChanges();
        //        return RedirectToAction("Indexclassadd");
        //    }
        //    return View(classAdd);
        //}
        //public IActionResult DeleteClassAdd(int? AdmissionID)
        //{
        //    var obj = _Context.classadd.Find(AdmissionID);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _Context.classadd.Remove(obj);
        //    _Context.SaveChanges();
        //    return RedirectToAction("Indexclassadd");
        //}


    }
}







