﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            IEnumerable<Patient> Adlist = Context.patients;
            return View(Adlist);
        }
        public IActionResult AddAdmission()
        {
            ViewBag.getPatient = new SelectList(Context.patients, "PatientID", "PatientName");
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
                return RedirectToAction("ViewAdmission");
            }
            return View(admission);
        } 
        //public IActionResult AddBed()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddBed(Bed bed) 
        //{ 
        //    if (ModelState.IsValid) 
        //    {
        //        Context.bed.Add(bed);
        //        Context.SaveChanges();
        //        return RedirectToAction("AddBed");
        //    }
        //    return View(bed);
        //} 
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
                return RedirectToAction("ViewAdmission");
            }
            return View(patients);
        }
        public IActionResult updateAdmission(int?ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var patients = Context.admission.Find(ID);
            if (patients == null)
            {
                return NotFound();
            }
            return View(patients);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdmission(int ID,Admission admission)
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
            IEnumerable<Vitals> vitlist = Context.vitals;
            return View(vitlist);
        }
        public IActionResult UpdateVital(int? ID)
        {

            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = Context.vitals.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            return View(objList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateVitals(int ID, Vitals vitals)
        {


            Context.vitals.Update(vitals);
            Context.SaveChanges();
            return RedirectToAction("ViewVitals");
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
            IEnumerable<MedicationAdministration> medAdminlist = Context.medAdmin;
            return View(medAdminlist);
        }
        public IActionResult ViewDischargedPatient()
        {
            IEnumerable<DischargePatient> dischargelist = Context.discharge;
            return View(dischargelist);
        }
        public IActionResult AddDischargedPatient()
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
        public IActionResult UpdateDischargedPatient(int? ID)
        {

            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = Context.vitals.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            return View(objList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDischargedPatient(int ID, DischargePatient discharge)
        {


            Context.discharge.Update(discharge);
            Context.SaveChanges();
            return RedirectToAction("ViewDischargedPatient");
        }

    }
}
