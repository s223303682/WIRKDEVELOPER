using Microsoft.AspNetCore.Mvc;
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



        public IActionResult updateBookingPatient(int? ID)
    {
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "AnaesthesiologistID", "ApplicationUser");
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
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "AnaesthesiologistID", "ApplicationUser");
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
            char[] splitCharacters = new[] { ',' };

            var bookings = _Context.bookings
                .Include(b => b.OperationTheatre) // Ensure related entities are included
                .Include(b => b.Anaesthesiologist) // Include the anaesthesiologist
                .Select(b => new BookingViewModel
                {
                    BookingID = b.BookingID,
                    PatientName = b.Name,
                    PatientSurname = b.Surname,
                    Gender = b.Gender,
                    EmailAddress = b.EmailAddress,
                    Date = b.Date,
                    Time = b.Time,
                    OperationTheatreName = b.OperationTheatre != null ? b.OperationTheatre.OperationTheatreName : null,
                    AnaName = b.OperationTheatre != null ? b.Anaesthesiologist.ApplicationUser.FirstName: null,
                    TreatmentCodes = string.IsNullOrEmpty(b.TreatmentCodeIDs)
                        ? new List<string>()
                        : b.TreatmentCodeIDs.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries).ToList()
                 // Assuming ApplicationUser has a UserName property
                })
                .ToList();

            return View(bookings);
        }

        public IActionResult CreateBooking(string? name, string surname, string gender, string? email)
        {
            // Populate dropdowns for the view
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "ApplicationUser"); // This remains the same

            // Create a new Booking object with pre-filled fields if provided
            var bookingModel = new Booking
            {
                Name = name,
                Surname = surname,
                Gender = gender,
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
                booking.TreatmentCodeIDs = string.Join(",", TreatmentCodeIDs);
                _Context.bookings.Add(booking);
                _Context.SaveChanges();
                return RedirectToAction("BookingList");
            }

            // Repopulate dropdowns if validation fails
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName", booking.OperationTheatreID);
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE", booking.TreatmentCodeIDs);
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "ApplicationUser", booking.AnaesthesiologistID); // Re-populate
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
