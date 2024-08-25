﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using System.Web.WebPages.Html;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;
namespace WIRKDEVELOPER.Controllers
{
    public class SurgeonController: Controller
    {
        private readonly ApplicationDBContext _Context;

        public SurgeonController(ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult ToDoList()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            return View();
        }
        public IActionResult SearchPatient(string searchby, string search)
        {
            if (searchby == "Gender")
            {
                return View(_Context.patients.Where(x => x.PatientName == search || search == null).ToList());
            }
            else if (searchby == "Gender")
            {
                return View(_Context.patients.Where(x => x.Gender == search || search == null).ToList());
            }
            else
            {
                return View("AddPatient");

            }
        }
        public IActionResult AddPatient()
        {
            return View();
        }
        public IActionResult PrescriptionList()
        {
            var prescriptions = _Context.prescriptions.ToList();
            return View(prescriptions);
        }
        // GET: Prescription/Create
        public IActionResult CreatePrescription(int bookingID, string name, string gender, string email)
        {
            ViewBag.getPharmacyMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            var model = new Prescription
            {
                Name = name,
                Gender = gender,
                Email = email,
                // Initialize other fields as needed
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult CreatePrescription(Prescription model)
        {
            if (ModelState.IsValid)
            {
                // Save the prescription to the database or perform the required action
                _Context.prescriptions.Add(model);
                ViewBag.getPharmacyMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
                _Context.SaveChanges();

                return RedirectToAction("PrescriptionList"); // Redirect to a view displaying the list of prescriptions or another appropriate action
            }

            return View(model); // Redisplay the form with validation errors if any
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updatePrescription(Prescription prescription)
        {
            
            _Context.prescriptions.Update(prescription);
            _Context.SaveChanges();
            return RedirectToAction("PrescriptionList");
        }
        public IActionResult DeletePrescription(int? ID)
        {
            var list = _Context.prescriptions.Find(ID);
            if (list == null)
            {
                return NotFound();
            }
            _Context.prescriptions.Remove(list);
            _Context.SaveChanges();
            return RedirectToAction("PrescriptionList");

        }
        public IActionResult BookingPatientList()
        {
            IEnumerable<BookingNewPatient> list = _Context.bookingNewPatients
                .Include(a => a.OperationTheatre)
               
                .Include(s => s.treatmentCode);


            return View(list);
        }
        public IActionResult CreateBookingPatient()
        {
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBookingPatient(BookingNewPatient bookingNewPatient)
        {

           _Context.bookingNewPatients.Add(bookingNewPatient);
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            
            _Context.SaveChanges();
            return RedirectToAction("BookingPatientList");
        }
        //return View(bookingNew);



        public IActionResult updateBookingPatient(int? ID)
    {
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "ApplicationUser");
            if (ID == null || ID == 0)
        {
            return NotFound();
        }
        var list = _Context.bookingNewPatients.Find(ID);
        if (list == null)
        {
            return NotFound();
        }

        return View(list);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult updateBookingPatient(BookingNewPatient bookingNewPatient)
    {
        //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //bookSurgery.PatientID = user;
        _Context.bookingNewPatients.Update(bookingNewPatient);
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "ApplicationUser");
            _Context.SaveChanges();
        return RedirectToAction("BookingPatientList");
    }
    public IActionResult DeleteBookingPatient(int? ID)
    {
        var list = _Context.bookingNewPatients.Find(ID);
        if (list == null)
        {
            return NotFound();
        }
        _Context.bookingNewPatients.Remove(list);
        _Context.SaveChanges();
        return RedirectToAction("BookingPatientList");

    }
        public IActionResult BookingList()
        {
            var bookings = _Context.bookings
                .Include(b => b.OperationTheatre)
                .Include(b => b.TreatmentCode)
                .Include(b => b.Addm) // Assuming you have a Patient entity
                .ToList();
            return View(bookings);
        }

        public IActionResult CreateBooking()
        {
            // Populate dropdowns for the view
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getPatient = new SelectList(_Context.addm, "AddmID", "PatientName"); 

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _Context.bookings.Add(booking);
                    _Context.SaveChanges();
                    return RedirectToAction("BookingList");
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) and handle accordingly
                    ModelState.AddModelError("", "An error occurred while creating the booking. Please try again.");
                }
            }

            // Re-populate dropdowns in case of validation errors
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName", booking.OperationTheatreID);
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE", booking.TreatmentCodeID);
            ViewBag.getPatient = new SelectList(_Context.addm, "AddmID", "PatientName", booking.Addm);

            return View(booking);
        }
        public IActionResult updateBooking(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.bookings.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateBooking(Booking booking)
        {
            
            _Context.bookings.Update(booking);
            _Context.SaveChanges();
            return RedirectToAction("BookingList");
        }
        public IActionResult DeleteBooking(int? ID)
        {
            var list = _Context.bookings.Find(ID);
            if (list == null)
            {
                return NotFound();
            }
            _Context.bookings.Remove(list);
            _Context.SaveChanges();
            return RedirectToAction("BookingList");

        }
    } 
}
