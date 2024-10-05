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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WIRKDEVELOPER.Models.sendemail;  // Check if this is the correct namespace

using Newtonsoft.Json;
using WIRKDEVELOPER.Models.sendemail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Web.Helpers;
using System.Xml.Linq;
using System.Diagnostics;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace WIRKDEVELOPER.Controllers
{
    public class SurgeonController: Controller
    {
        private readonly ApplicationDBContext _Context;
        private readonly EmailService _emailService;
        public SurgeonController(ApplicationDBContext applicationDBContext, EmailService emailService)
        {
            _Context = applicationDBContext;
            _emailService = emailService;

        }
        public IActionResult ToDoList()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            return View();
        }
        public IActionResult IndexViewPatient(String searchpatient)
        {

            //var patient = _Context.admission.Where(e => e.Date == searchDate.Date).ToList();
            //return View(patient);
            var patient = _Context.addm.Include(a => a.Ward).Include(a => a.Bed).Include(a => a.Patient).Where(e => e.Patient.PatientIDNO == searchpatient).ToList();
            return View(patient);
        }
        public IActionResult ViewPatientRec()
        {
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            ViewBag.getWard = new SelectList(_Context.ward, "WardID", "WardName");
            ViewBag.getBed = new SelectList(_Context.bed, "BedID", "BedNumber");

            return View();

        }

        public IActionResult AddPatient()
        {
            return View();
        }

        public IActionResult CreatePrescription(int bookingId, string name, string surname, string gender, string email)
        {
            var viewModel = new PrescriptionViewModel
            {
                Name = name,
                surname = surname,
                Gender = gender,
                Email = email
            };

            // Get list of medications
            ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescription(PrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prescription = new Prescription
                {
                    Name = model.Name,
                    Surname = model.surname,
                    Gender = model.Gender,
                    Email = model.Email,
                    Date = model.Date,
                    Prescriber = model.Prescriber,
                    Urgent = model.Urgent,
                    Status = model.Status,
                    PrescriptionMedications = model.Medications.Select(m => new PrescriptionMedication
                    {
                        PharmacyMedicationID = m.PharmacyMedicationID,
                        Quantity = m.Quantity,
                        Instructions = m.Instructions
                    }).ToList()
                };

                _Context.prescriptions.Add(prescription);
                _Context.SaveChanges();

                return RedirectToAction("PrescriptionList"); // After creation, redirect to the list of prescriptions
            }

            // If validation fails, re-populate the medications dropdown
            ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();
            return View(model);
        }


        // GET: Prescription/List
        public IActionResult PrescriptionList()
        {
            var prescriptions = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                    .ThenInclude(pm => pm.PharmacyMedication) // Include related PharmacyMedication data
                .Select(p => new PrescriptionViewModel
                {
                    Name = p.Name,
                    surname = p.Surname,
                    Gender = p.Gender,
                    Email = p.Email,
                    Date = p.Date,
                    Prescriber = p.Prescriber,
                    Urgent = p.Urgent,
                    Status = p.Status,
                    Medications = p.PrescriptionMedications.Select(m => new PrescriptionMedicationViewModel
                    {
                        PharmacyMedicationID = m.PharmacyMedicationID,
                        Quantity = m.Quantity,
                        Instructions = m.Instructions
                    }).ToList()
                }).ToList();

            return View(prescriptions);
        }
        // GET: Prescription/Edit/{id}
        public IActionResult updatePrescription(int id)
        {
            // Fetch the prescription by ID, including its medications
            var prescription = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                .ThenInclude(pm => pm.PharmacyMedication)
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }

            // Map to ViewModel
            var viewModel = new PrescriptionViewModel
            {
                PrescriptionViewModelID = prescription.PrescriptionID,
                Name = prescription.Name,
                surname = prescription.Surname,
                Gender = prescription.Gender,
                Email = prescription.Email,
                Date = prescription.Date,
                Prescriber = prescription.Prescriber,
                Urgent = prescription.Urgent,
                Status = prescription.Status,
                Medications = prescription.PrescriptionMedications.Select(m => new PrescriptionMedicationViewModel
                {
                    PharmacyMedicationID = m.PharmacyMedicationID,
                    Quantity = m.Quantity,
                    Instructions = m.Instructions
                }).ToList()
            };

            // Pass medications for dropdown
            ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updatePrescription(PrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prescription = _Context.prescriptions
                    .Include(p => p.PrescriptionMedications)
                    .FirstOrDefault(p => p.PrescriptionID == model.PrescriptionViewModelID);

                if (prescription == null)
                {
                    return NotFound();
                }

                // Update prescription properties
                prescription.Name = model.Name;
                prescription.Surname = model.surname;
                prescription.Gender = model.Gender;
                prescription.Email = model.Email;
                prescription.Date = model.Date;
                prescription.Prescriber = model.Prescriber;
                prescription.Urgent = model.Urgent;
                prescription.Status = model.Status;

                // Update medications
                prescription.PrescriptionMedications.Clear();
                prescription.PrescriptionMedications = model.Medications.Select(m => new PrescriptionMedication
                {
                    PharmacyMedicationID = m.PharmacyMedicationID,
                    Quantity = m.Quantity,
                    Instructions = m.Instructions
                }).ToList();

                _Context.SaveChanges();
                return RedirectToAction("PrescriptionList");
            }

            // Re-populate medications if validation fails
            ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();
            return View(model);
        }

        // GET: Prescription/Delete/{id}
        public IActionResult DeletePrescription(int id)
        {
            var prescription = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }

            _Context.prescriptions.Remove(prescription);
            _Context.SaveChanges();

            return RedirectToAction("PrescriptionList");
        }


        public IActionResult BookingPatientList()
        {
            IEnumerable<BookingNewPatient> list = _Context.bookingNewPatients
                .Include(a => a.OperationTheatre)
                .Include(a=> a.Anaesthesiologist)
                .Include(s => s.treatmentCode);


            return View(list);
        }
        public IActionResult CreateBookingPatient()
        {
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            var anaesthesiologists = _Context.Anaesthesiologists
             .Select(a => new SelectListItem
             {
                 Value = a.UserId.ToString(),
                 Text = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}"
             }).ToList();

            ViewBag.getAnaesthesiologist = anaesthesiologists; // Assign the list directly
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBookingPatient(BookingNewPatient bookingNewPatient)
        {
            _Context.bookingNewPatients.Add(bookingNewPatient);
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            var anaesthesiologists = _Context.Anaesthesiologists
             .Select(a => new SelectListItem
             {
                 Value = a.UserId.ToString(),
                 Text = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}"
             }).ToList();

            ViewBag.getAnaesthesiologist = anaesthesiologists; // Assign the list directly
            _Context.SaveChanges();
            string subject = "Booking Confirmation";
            string body = $"Dear {bookingNewPatient.BookingNewPatientName}, your booking is confirmed for {bookingNewPatient.Date}.";

            await _emailService.SendEmailAsync(bookingNewPatient.Email, subject, body);
            return RedirectToAction("BookingPatientList");

        }



        public IActionResult updateBookingPatient(int? ID)
    {
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            var anaesthesiologists = _Context.Anaesthesiologists
              .Select(a => new SelectListItem
              {
                  Value = a.UserId.ToString(),
                  Text = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}"
              }).ToList();

            ViewBag.getAnaesthesiologist = anaesthesiologists; // Assign the list directly
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
            var anaesthesiologists = _Context.Anaesthesiologists
              .Select(a => new SelectListItem
              {
                  Value = a.UserId.ToString(),
                  Text = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}"
              }).ToList();

            ViewBag.getAnaesthesiologist = anaesthesiologists; // Assign the list directly
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
        public IActionResult CreateBooking(int addmId, string name, string surname, string gender, string email)
        {
            var viewModel = new BookingViewModel
            {
                Name = name,
                Surname = surname,
                Gender = gender,
                Email = email
            };

            // Populate Operation Theatre dropdown
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");

            // Fetch the list of Anaesthesiologists
            var anaesthesiologists = _Context.Anaesthesiologists
                .Select(a => new SelectListItem
                {
                    Value = a.UserId.ToString(), // Ensure this is a string
                    Text = a.ApplicationUser.FirstName + " " + a.ApplicationUser.LastName
                })
                .ToList();

            // Assign the list to ViewBag
            ViewBag.getAnaesthesiologist = anaesthesiologists; // No need to create a SelectList again

            // Populate TreatmentCode dropdown
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = new Booking
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Gender = model.Gender,
                    Email = model.Email,
                    Date = model.Date,
                    Time = model.Time,
                    Status = model.Status,
                    OperationTheatreID = model.OperationTheatreID,
                    UserId = model.UserId // Set the correct foreign key
                };

                // Add selected TreatmentCodes
                foreach (var treatmentCodeID in model.TreatmentCodeIDs)
                {
                    booking.BookingTreatmentCodes.Add(new BookingTreatmentCode
                    {
                        TreatmentCodeID = treatmentCodeID
                    });
                }

                _Context.bookings.Add(booking);
                _Context.SaveChanges();

                return RedirectToAction("BookingList"); // Redirect to the list of bookings
            }

            // If model state is invalid, repopulate the dropdowns
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");

            var anaesthesiologists = _Context.Anaesthesiologists
                .Select(a => new SelectListItem
                {
                    Value = a.UserId.ToString(),
                    Text = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}"
                }).ToList();

            ViewBag.getAnaesthesiologist = anaesthesiologists; // Assign the list directly
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");

            return View(model);
        }
        public IActionResult BookingList()
        {
            var bookings = _Context.bookings
                .Include(b => b.OperationTheatre) // Include navigation properties if needed
                .Include(b => b.Anaesthesiologist)
                .ToList();

            return View(bookings);
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
