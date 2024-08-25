using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Dashboard()
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
        public IActionResult AddAdmissions()
        {
            //ViewBag.Patients = (from U in Context.Users
            //                    join UR in Context.UserRoles on U.Id equals UR.UserId
            //                    join R in Context.Roles on UR.RoleId equals R.Id
            //                    where R.Name == "Patient"
            //                    select U).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAdmission(/*[Bind("AdmissionID,PatientID,PatientGender,PatientEmail,PatientPhone,Address1,Address2,Province,City,Suburb,PostalCode,Ward,Condition,Allergies,MedicationName,TreatmentCode")]*/Admission admission) 
        { 
            if (ModelState.IsValid) 
            {
                //ViewBag.Date = DateTime.Now.ToString("dd/mm/yyyy");
                //ViewBag.Time = DateTime.Now.ToString("HH:MM");
                Context.admission.Add(admission);
                Context.SaveChanges();
                return RedirectToAction("ViewAddmission");
            }
            //ViewBag.Patients = (from U in Context.Users
            //                    join UR in Context.UserRoles on U.Id equals UR.UserId
            //                    join R in Context.Roles on UR.RoleId equals R.Id
            //                    where R.Name == "Patient"
            //                    select U).ToList();
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
                return RedirectToAction("AddBed");
            }
            return View(bed);
        } 
        public IActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatient(Patient patients) 
        { 
            if (ModelState.IsValid) 
            {
                Context.patients.Add(patients);
                Context.SaveChanges();
                return RedirectToAction("AddPatient");
            }
            return View(patients);
        }
        public IActionResult updateAdmission(int?ID)
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
            //ViewBag.Patients = (from U in Context.Users
            //                    join UR in Context.UserRoles on U.Id equals UR.UserId
            //                    join R in Context.Roles on UR.RoleId equals R.Id
            //                    where R.Name == "Patient"
            //                    select U).ToList();
            return View(patient);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdmission(Admission admission)
        {
            if (ModelState.IsValid)
            {
                Context.admission.Update(admission);
                Context.SaveChanges();
                return RedirectToAction("ViewAdmission");
            }
            return View(admission);
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
                return RedirectToAction("AddCondition");
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
                return RedirectToAction("ViewVitals");
            }
            return View(vitals);
        }
        public IActionResult ViewVitals()
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
                return RedirectToAction("ViewmedAdmin");
            }
            return View();
        }
        public IActionResult ViewmedAdmin()
        {
            IEnumerable<MedicationAdministration> medAdmin = Context.medAdmin;
            return View(medAdmin);
        }
        public IActionResult ViewDischargedPatient()
        {
            IEnumerable<DischargePatient> discharge = Context.discharge;
            return View(discharge);
        }
        public IActionResult AddDischargePatient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDischargedPatient(DischargePatient discharge)
        {
            if (ModelState.IsValid)
            {
                Context.discharge.Add(discharge);
                Context.SaveChanges();
                return RedirectToAction("ViewDischargePatient");
            }
            return View(discharge);
        }

    }
}
