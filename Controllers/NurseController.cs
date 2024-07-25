using Microsoft.AspNetCore.Mvc;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;

namespace WIRKDEVELOPER.Controllers
{
    public class NurseController : Controller
    {
        private readonly ApplicationDBContext Context;
        public NurseController(ApplicationDBContext DbContext)
        {
            Context = DbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewSchedule()
        {
            IEnumerable<Patient> patient = Context.patients;
            return View(patient);
        }
        public IActionResult ViewAdmission()
        {
            IEnumerable<Patient> patient = Context.patients;
            return View(patient);
        }
        public IActionResult AddAddmission()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAdmission(Admission admission) 
        { 
            if (ModelState.IsValid) 
            {
                Context.admission.Add(admission);
                Context.SaveChanges();
                return RedirectToAction("ViewAddmission");
            }
            return View(admission);
        } 
        public IActionResult AddBed()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBed(Bed bed) 
        { 
            if (ModelState.IsValid) 
            {
                Context.bed.Add(bed);
                Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(bed);
        }
        public IActionResult updateAddmission(int?ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var patient = Context.admission.Find(ID);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);

        }
        public IActionResult AddCondition()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCondition(Condition conditions)
        {
            if (ModelState.IsValid)
            {
                Context.conditions.Add(conditions);
                Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(conditions);
        }
        public IActionResult AddVitals()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddVitals(Vitals vitals)
        {
            if (ModelState.IsValid)
            {
                Context.vitals.Add(vitals);
                Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(vitals);
        }
        public IActionResult ViewVital()
        {
            IEnumerable<Vitals> vitals = Context.vitals;
            return View(vitals);
        } 
        public IActionResult ViewPrescription()
        {
            IEnumerable<Prescription> prescriptions = Context.prescriptions;
            return View(prescriptions);
        } 
        public IActionResult AddmedAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddmedAdmin(MedicationAdministration medAdmin)
        {
            if (ModelState.IsValid) 
            {
                Context.medAdmin.Add(medAdmin);
                Context.SaveChanges();
                return RedirectToAction("");
            }
            return View();
        }
        public IActionResult ViewmedAdmin()
        {
            IEnumerable<MedicationAdministration> medAdmin = Context.medAdmin;
            return View(medAdmin);
        }

    }
}
