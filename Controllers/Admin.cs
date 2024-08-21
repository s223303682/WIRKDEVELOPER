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
        public IActionResult ActiveList()
        {
            IEnumerable<Active> list = _Context.active;
            return View(list);
        }
        public IActionResult CreateActive()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateActive(Active active)
        {
            if (ModelState.IsValid)
            {
                _Context.active.Add(active);
                _Context.SaveChanges();
                return RedirectToAction("ActiveList");
            }
            return View(active);
        }
        public IActionResult ScheduleList()
        {
            IEnumerable<Schedule> list = _Context.schedules;
            return View(list);
        }
        public IActionResult CreateSchedule()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSchedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _Context.schedules.Add(schedule);
                _Context.SaveChanges();
                return RedirectToAction("ScheduleList");
            }
            return View(schedule);
        }
        public IActionResult PatientList()
        {
            IEnumerable<Patient> list = _Context.patients;
            return View(list);
        }
        public IActionResult CreatePatient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _Context.patients.Add(patient);
                _Context.SaveChanges();
                return RedirectToAction("PatientList");
            }
            return View(patient);
        }
        public IActionResult updatePatient(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.patients.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updatePatient(Patient patient)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.patients.Update(patient);
            _Context.SaveChanges();
            return RedirectToAction("PatientList");
        }
        public IActionResult TreatmentCodeList()
        {
            IEnumerable<TreatmentCode> list = _Context.treatmentCodes;
            return View(list);
        }
        public IActionResult CreateTreatmentCode()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTreatmentCode(TreatmentCode treatmentCode)
        {
            if (ModelState.IsValid)
            {
                _Context.treatmentCodes.Add(treatmentCode);
                _Context.SaveChanges();
                return RedirectToAction("TreatmentCodeList");
            }
            return View(treatmentCode);
        }
        public IActionResult updateTreatmentCode(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.treatmentCodes.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateTreatmentCode(TreatmentCode treatmentCode)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.treatmentCodes.Update(treatmentCode);
            _Context.SaveChanges();
            return RedirectToAction("TreatmentCodeList");
        } 
        public IActionResult ViewCondition()
        {
            IEnumerable<Condition> conditions = _Context.conditions;
            return View(conditions);
        }
        public IActionResult AddCondiion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCondition(Condition conditions)
        {
            if (ModelState.IsValid)
            {
                _Context.conditions.Add(conditions);
                _Context.SaveChanges();
                return RedirectToAction("AddCondition");
            }
            return View(conditions);
        }
        public IActionResult updateCondition(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.treatmentCodes.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateCondition(Condition conditions)
        {
            _Context.conditions.Update(conditions);
            _Context.SaveChanges();
            return RedirectToAction("ViewCondition");
        }
        public IActionResult ViewVitals()
        {
            IEnumerable<PatientVitals> patientVitals = _Context.patientVitals;
            return View(patientVitals);
        }
        public IActionResult AddVitals()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddVitals(PatientVitals patientVitals)
        {
            if (ModelState.IsValid)
            {
                _Context.patientVitals.Add(patientVitals);
                _Context.SaveChanges();
                return RedirectToAction("AddVitals");
            }
            return View(patientVitals);
        }
        public IActionResult updateVitals(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.treatmentCodes.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateVitals(PatientVitals patientVitals)
        {
            _Context.patientVitals.Update(patientVitals);
            _Context.SaveChanges();
            return RedirectToAction("ViewVitals");
        }
        public IActionResult ViewBeds()
        {
            IEnumerable<Bed> beds = _Context.beds;
            return View(beds);
        }
        public IActionResult AddBeds()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBeds(Bed beds)
        {
            if (ModelState.IsValid)
            {
                _Context.beds.Add(beds);
                _Context.SaveChanges();
                return RedirectToAction("AddBed");
            }
            return View(beds);
        }
        public IActionResult updateBeds(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.treatmentCodes.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateBed(Bed beds)
        {
            _Context.beds.Update(beds);
            _Context.SaveChanges();
            return RedirectToAction("ViewBed");
        }

    }
}
