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
        public IActionResult PrescriptionList()
        {
            var prescriptions = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                    .ThenInclude(pm => pm.PharmacyMedication) // Include related PharmacyMedication data
                .Select(p => new PrescriptionViewModel
                {
                    Name = p.Name,
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




        public IActionResult CreatePrescription(int bookingID, string name, string gender, string email)
        {
            // Fetch the booking details and retrieve patient data
            var booking = _Context.bookings
                .Where(b => b.BookingID == bookingID)
                .Select(b => new
                {
                    b.Addm.Patient.PatientName,
                    b.Addm.Patient.Gender,
                    b.EmailAddress,
                    b.Addm.PatientID
                })
                .FirstOrDefault();

            if (booking == null)
            {
                return NotFound(); // Handle case where booking doesn't exist
            }

            // Get patient's known allergies using PatientID
            var patientAllergies = _Context.addm
                .Where(a => a.PatientID == booking.PatientID)
                .Select(a => a.AnAllergies.Active.ActiveName)
                .ToList();

            // Get available medications with active ingredients
            var medications = _Context.PharmacyMedicationIngredients
                .Select(pm => new
                {
                    pm.PharmacyMedicationIngredientId,
                    pm.PharmacyMedication.PharmacyMedicationName,
                    pm.Active.ActiveName
                })
                .ToList();

            // Serialize medications and allergies to JSON
            ViewBag.Medications = JsonConvert.SerializeObject(medications);
            ViewBag.PatientAllergies = JsonConvert.SerializeObject(patientAllergies);

            // Create the prescription view model
            var model = new PrescriptionViewModel
            {
                Name = booking.PatientName,
                Gender = booking.Gender,
                Email = booking.EmailAddress
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreatePrescription(PrescriptionViewModel model, int bookingID)
        {
            if (ModelState.IsValid)
            {
                // Fetch the PatientID from the booking table using bookingID
                var patientID = _Context.bookings
                    .Where(b => b.BookingID == bookingID)
                    .Select(b => b.AddmID)
                    .FirstOrDefault();

                if (patientID == 0)
                {
                    ModelState.AddModelError("", "Invalid booking ID.");
                    // Return the same view with errors if patientID is not found
                    return View(model);
                }

                // Get patient's known allergies using PatientID
                var patientAllergies = _Context.addm
                    .Where(a => a.PatientID == patientID) // Use the fetched PatientID
                    .Select(a => a.AnAllergies.Active.ActiveName)
                    .ToList();

                // Check for allergies in medications (similar to previous logic)

                // Create a new Prescription entity
                var prescription = new Prescription
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    Email = model.Email,
                    Date = model.Date,
                    Prescriber = model.Prescriber,
                    Urgent = model.Urgent,
                    Status = model.Status,
                    IgnoreReason = model.IgnoreReason // Add the reason for ignoring allergies if applicable
                };

                // Add the prescription to the database
                _Context.prescriptions.Add(prescription);
                _Context.SaveChanges(); // Save to generate the PrescriptionID

                // Add the medications
                foreach (var medication in model.Medications)
                {
                    var prescriptionMedication = new PrescriptionMedication
                    {
                        PrescriptionID = prescription.PrescriptionID, // Use the ID generated by the database
                        PharmacyMedicationID = medication.PharmacyMedicationID,
                        Quantity = medication.Quantity,
                        Instructions = medication.Instructions
                    };
                    _Context.prescriptionMedications.Add(prescriptionMedication);
                }

                _Context.SaveChanges();

                return RedirectToAction("PrescriptionList");
            }

            // If the model is invalid, return the same view with validation errors
            var medications = _Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList();
            ViewBag.Medications = JsonConvert.SerializeObject(medications);

            // Fetch the PatientID again in case of validation errors
            var patientIDForErrors = _Context.bookings
                .Where(b => b.BookingID == bookingID)
                .Select(b => b.AddmID)
                .FirstOrDefault();

            if (patientIDForErrors != 0)
            {
                var patientAllergies = _Context.addm
                    .Where(a => a.PatientID == patientIDForErrors)
                    .Select(a => a.AnAllergies.Active.ActiveName)
                    .ToList();
                ViewBag.PatientAllergies = JsonConvert.SerializeObject(patientAllergies);
            }

            return View(model);
        }



        // GET: Prescription/Update/5
        // GET: Prescription/Update/5
        public IActionResult UpdatePrescription(int id)
        {
            // Fetch the existing prescription by ID
            var prescription = _Context.prescriptions
                .Include(p => p.PrescriptionMedications) // Include medications
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }

            // Get available medications for dropdown
            var medications = _Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList();

            // Serialize medications to JSON
            ViewBag.Medications = JsonConvert.SerializeObject(medications);

            var model = new PrescriptionViewModel
            {
                PrescriptionViewModelID = prescription.PrescriptionID,
                Name = prescription.Name,
              
                Gender = prescription.Gender,
                Email = prescription.Email,
                Date = prescription.Date,
                Prescriber = prescription.Prescriber,
                Urgent = prescription.Urgent,
                Status = prescription.Status,
                Medications = prescription.PrescriptionMedications.Select(pm => new PrescriptionMedicationViewModel
                {
                    PharmacyMedicationID = pm.PharmacyMedicationID,
                    Quantity = pm.Quantity,
                    Instructions = pm.Instructions
                }).ToList()
            };

            return View(model);
        }

        // POST: Prescription/Update
        [HttpPost]
        public IActionResult UpdatePrescription(PrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Fetch the existing prescription from the database
                var prescription = _Context.prescriptions.Find(model.PrescriptionViewModelID);
                if (prescription == null)
                {
                    return NotFound();
                }

                // Update only the status field
                prescription.Status = model.Status;

                // Save changes to the database
                _Context.SaveChanges();

                return RedirectToAction("PrescriptionList"); // Redirect to the list after update
            }

            // If the model is invalid, re-fetch medications and return the view with errors
            var medications = _Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList();
            ViewBag.Medications = JsonConvert.SerializeObject(medications);
            return View(model);
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
        public async Task<IActionResult> CreateBookingPatient(BookingNewPatient bookingNewPatient)
        {
                _Context.bookingNewPatients.Add(bookingNewPatient);
                ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
                ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");

                _Context.SaveChanges();
                string subject = "Booking Confirmation";
                string body = $"Dear {bookingNewPatient.BookingNewPatientName}, your booking is confirmed for {bookingNewPatient.Date}.";

                await _emailService.SendEmailAsync(bookingNewPatient.Email, subject, body);
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
            // Define the split characters as a local variable
            char[] splitCharacters = new[] { ',' };

            var bookings = _Context.bookings
                .Include(b => b.Addm)
                .Include(b => b.OperationTheatre) // Ensure related entities are included
                .Select(b => new BookingViewModel
                {
                    BookingID = b.BookingID,
                    //PatientName = b.Name /*+ " " + b.Surname*/,
                    //Gender = b.Gender,
                    EmailAddress = b.EmailAddress,
                    Date = b.Date,
                    Time = b.Time,
                    OperationTheatreName = b.OperationTheatre != null ? b.OperationTheatre.OperationTheatreName : null,
                    TreatmentCodes = string.IsNullOrEmpty(b.TreatmentCodeIDs)
                        ? new List<string>()
                        : b.TreatmentCodeIDs.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Anaestesiologist = b.Anaestesiologist
                })
                .ToList();

            return View(bookings);
        }






        public IActionResult CreateBooking(int? AddmID, string? name, string? email)
        {
            // Populate dropdowns for the view
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");

            // Retrieve Addm records along with their associated Patient
            var patients = _Context.addm.Include(a => a.Patient)
                                         .Select(a => new
                                         {
                                             AddmID = a.AddmID,
                                             PatientName = a.Patient.PatientName /*+ " " + a.Patient.PatientSurname*/
                                         })
                                         .ToList();

            ViewBag.getPatient = new SelectList(patients, "AddmID", "PatientName");

            // Create a new Booking object with pre-filled fields if provided
            var bookingModel = new Booking
            {
                AddmID = AddmID,
                Name = name,
                //Surname = surname,
                EmailAddress = email
            };

            return View(bookingModel); // Pass the Booking model to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking, string[] TreatmentCodeIDs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Convert the array of TreatmentCodeIDs to a comma-separated string
                    booking.TreatmentCodeIDs = string.Join(",", TreatmentCodeIDs);

                    _Context.bookings.Add(booking);
                    _Context.SaveChanges();
                    return RedirectToAction("BookingList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the booking. Please try again.");
                }
            }

            // Re-populate dropdowns in case of validation errors
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName", booking.OperationTheatreID);
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE", booking.TreatmentCodeIDs);
            var patients = _Context.addm.Include(a => a.Patient)
                                          .Select(a => new { AddmID = a.AddmID, PatientName = a.Patient.PatientName })
                                          .ToList();

            ViewBag.getPatient = new SelectList(patients, "AddmID", "PatientName");

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
