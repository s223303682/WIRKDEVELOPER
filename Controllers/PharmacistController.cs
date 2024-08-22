using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
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
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult CreateMedication()
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
                return RedirectToAction("MedicationList");
            }
            return View(medication);
           
        }
        public IActionResult MedicationList()
        {
            IEnumerable<Medication> list = _Context.medications;
            return View(list);
        }
   
        //public async Task<IActionResult> AcceptPrescription(int? ID)
        //{
        //    var prescriptions = _Context.prescriptions.Find(ID);
        //    if (prescriptions != null)
        //    {
        //        prescriptions.status = "Accepted";
        //        _Context.prescriptions.Update(prescriptions);
        //        await _Context.SaveChangesAsync();
        //        TempData["Info"] = "Dispsed";
        //        //var patient = _Context.Users.Where(a => a.Id == prescriptions.PatientID).FirstOrDefault();
                
        //    }
        //    ViewData["PatientID"] = new SelectList(_Context.Users, "Id", "Id", prescriptions.PatientID);
        //    return View(prescriptions);
        //}
        public IActionResult PharmPrescriptionList()
        {
            IEnumerable<Prescription> list = _Context.prescriptions;
            return View(list);
        }
        public IActionResult AllPrescriptionList()
        {
            IEnumerable<Prescription> list = _Context.prescriptions;
            return View(list);
        }
        //public async Task<IActionResult> AcceptOrder(int? ID)
        //{
        //    var order = _Context.order.Find(ID);
        //    if (order != null)
        //    {
        //        order.Status = "Ordered";
        //        _Context.order.Update(order);
        //        await _Context.SaveChangesAsync();
        //        TempData["Ordered"] = "Dispensed";
        //        var patient = _Context.Users.Where(a => a.Id == order.Patient).FirstOrDefault();

        //    }
        //    ViewData["Patient"] = new SelectList(_Context.Users, "Id", "Id", order.Patient);
        //    return View(order);
        //}
        public IActionResult PharmIndexOrder()
        {
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication);
            return View(objList);
            //IEnumerable<Order> objList = _Context.order;
            //return View(objList);

        }
        public IActionResult AllIndexOrder()
        {
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication);
            return View(objList);
            //IEnumerable<Order> objList = _Context.order;
            //return View(objList);

        }
        public IActionResult IndexMedication()
        {
            IEnumerable<PharmacyMedication> objList = _Context.pharmacyMedications
                .Include(a => a.DosageForm)
                .Include(s => s.Schedule)
                .Include(a => a.Active);
            return View(objList);

        }
        public IActionResult AddMedication()
        {
            ViewBag.getDosage = new SelectList(_Context.dosageForms, "DosageFormID", "DosageFormName");
            ViewBag.getActive = new SelectList(_Context.active, "ActiveID", "ActiveName");
            ViewBag.getSchedule = new SelectList(_Context.schedules, "ScheduleId", "ScheduleName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedication(PharmacyMedication pharmacyMedications)
        {
            _Context.pharmacyMedications.Add(pharmacyMedications);
            ViewBag.getDosage = new SelectList(_Context.dosageForms, "DosageFormID", "DosageFormName");
            ViewBag.getActive = new SelectList(_Context.active, "ActiveID", "ActiveName");
            ViewBag.getSchedule = new SelectList(_Context.schedules, "ScheduleId", "ScheduleName");
            _Context.SaveChanges();
           

            return RedirectToAction("IndexMedication");


            //return View(pharmacyMedications);

        }
        public IActionResult updatePharmIndexOrder(int? ID)
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

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updatePharmIndexOrder(Order order)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.order.Update(order);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
            return RedirectToAction("PharmIndexOrder");
        }
        public IActionResult RejectPharmIndexOrder(int? ID)
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

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectPharmIndexOrder(Order order)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.order.Update(order);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
            return RedirectToAction("PharmIndexOrder");
        }
		public IActionResult updatePharmPrescription(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			var objList = _Context.prescriptions.Find(ID);
			if (objList == null)
			{
				return NotFound();
			}

			return View(objList);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult updatePharmPrescription(Prescription prescription)
		{
			//var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
			//bookSurgery.PatientID = user;
			_Context.prescriptions.Update(prescription);
			_Context.SaveChanges();
			return RedirectToAction("PharmPrescriptionList");
		}
        public IActionResult RejectPharmPrescription(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.prescriptions.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectPharmPrescription(Prescription prescription)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.prescriptions.Update(prescription);
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
            return RedirectToAction("PharmPrescriptionList");
        }
        public IActionResult IndexStock()
        {
            IEnumerable<PharmStock> objList = _Context.pharmStock
              .Include(a => a.PharmacyMedication);
            return View(objList);

        }
        public IActionResult AddStock()
        {
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStock(PharmStock pharmStocks)
        {
           
            _Context.pharmStock.Add(pharmStocks);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
                return RedirectToAction("IndexStock");
          
            //return View(pharmStocks);

        }
		public IActionResult UpdateStock(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			var objList = _Context.pharmStock.Find(ID);
			if (objList == null)
			{
				return NotFound();
			}
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateStock(PharmStock pharmStocks)
		{
			//var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
			//bookSurgery.PatientID = user;
			_Context.pharmStock.Update(pharmStocks);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
			return RedirectToAction("IndexStock");
		}
		public IActionResult ReciveStock()
		{
			IEnumerable<PharmStock> objList = _Context.pharmStock
			  .Include(a => a.PharmacyMedication);
			return View(objList);

		}








	}
}
