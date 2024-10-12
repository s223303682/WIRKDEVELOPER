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
using Rotativa.AspNetCore;
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
        // GET: Report (Default View)
        public ActionResult Index()
        {
            return View();
        }

        // POST: Generate the report based on the selected date range
        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            // Get data from database for the selected date range
            var bookings = _Context.bookings
                             .Where(b => b.Date >= startDate && b.Date <= endDate)
                             .ToList();

            // Pass the date range to the view
            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

            // Return the view with the report data
            return View(bookings);
        }

        // GET: Download the report as PDF
        public ActionResult DownloadReportAsPdf(DateTime startDate, DateTime endDate)
        {
            // Get the data you want to display in the PDF
            var bookings = _Context.bookings
                             .Where(b => b.Date >= startDate && b.Date <= endDate)
                             .ToList();

            // Generate the PDF using Rotativa
            return new ViewAsPdf("Index", bookings)
            {
                FileName = "SurgeryReport.pdf",
                PageMargins = { Left = 10, Right = 10, Top = 20, Bottom = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = "--footer-center \"Page: [page] of [toPage]\" --footer-right \"Generated: [date]\""
            };
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
        public IActionResult CreatePrescription(int bookingId, string name, string surname,string IDNumber, string gender, string email)
        {
            var viewModel = new PrescriptionViewModel
            {
                IDNumber = IDNumber,
                Name = name,
                Surname = surname,
                Gender = gender,
                Email = email,
                IgnoreReason = string.Empty,
                Medications = new List<PrescriptionMedicationViewModel>() // Initialize to avoid null reference
            };

            // Get list of medications
            ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescription(PrescriptionViewModel model, string ignoreReason)
        {
            // Get patient allergies from the database
            var patientAllergies = _Context.addm
                .Include(a => a.AnAllergies)
                .Where(a => a.Patient.PatientName == model.Name && a.Patient.PatientSurname == model.Surname)
                .Select(a => a.AnAllergies.ActiveID)
                .ToList();

            var allergyAlerts = new List<string>();

            // Check each medication against patient allergies
            foreach (var medication in model.Medications)
            {
                var medicationIngredients = _Context.pharmacyMedications
                    .Include(pm => pm.Ingredients)
                    .FirstOrDefault(pm => pm.PharmacyMedicationID == medication.PharmacyMedicationID)?.Ingredients;

                // Check for allergies
                if (medicationIngredients != null && medicationIngredients.Any(ing => patientAllergies.Contains(ing.ActiveID)))
                {
                    allergyAlerts.Add(medicationIngredients.First().PharmacyMedication.PharmacyMedicationName);
                }
            }

            // Handle allergy alerts
            if (allergyAlerts.Any())
            {
                ModelState.AddModelError("", "One or more medications may cause an allergy.");
                ViewBag.ShowAllergyAlert = true;
                ViewBag.AllergyAlerts = allergyAlerts;
                ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();
                return View(model); // Return view with alerts
            }

            // Proceed if model state is valid and no allergy alerts
            if (ModelState.IsValid)
            {
                var prescription = new Prescription
                {
                    IDNumber = model.IDNumber,
                    Name = model.Name,
                    Surname = model.Surname,
                    Gender = model.Gender,
                    Email = model.Email,
                    Date = model.Date,
                    Prescriber = model.Prescriber,
                    Urgent = model.Urgent,
                    Status = model.Status,
                    IgnoreReason = ignoreReason,
                    PrescriptionMedications = model.Medications.Select(m => new PrescriptionMedication
                    {
                        PharmacyMedicationID = m.PharmacyMedicationID,
                        Quantity = m.Quantity,
                        Instructions = m.Instructions
                    }).ToList()
                };

                // Add the prescription and save
                _Context.prescriptions.Add(prescription);
                _Context.SaveChanges();

                // Redirect to PrescriptionList after success
                return RedirectToAction("PrescriptionList");
            }

            // If model state is not valid
            ViewBag.PharmacyMedications = _Context.pharmacyMedications.ToList();
            return View(model);
        }


        // GET: Prescription/List
        public IActionResult PrescriptionList()
        {
            var prescriptions = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                    .ThenInclude(pm => pm.PharmacyMedication) // Include related PharmacyMedication data
                .Select(p => new Prescription
                {
                    PrescriptionID = p.PrescriptionID,
                    Name = p.Name,
                    Surname = p.Surname, // Ensure this property is included
                    Gender = p.Gender,
                    Email = p.Email,
                    Date = p.Date,
                    Prescriber = p.Prescriber,
                    Urgent = p.Urgent,
                    Status = p.Status,
                    PrescriptionMedications = p.PrescriptionMedications.Select(m => new PrescriptionMedication
                    {
                        PharmacyMedicationID = m.PharmacyMedicationID,
                        Quantity = m.Quantity,
                        Instructions = m.Instructions
                    }).ToList()
                }).ToList();

            return View(prescriptions);
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
        public IActionResult CreateBooking(int addmId, string name, string surname,string IDNumber, string gender, string email)
        {
            var viewModel = new BookingViewModel
            {
                IDNumber = IDNumber,
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
                    IDNumber = model.IDNumber,
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

                // Ensure that TreatmentCodeIDs is not null and has values
                if (model.TreatmentCodeIDs != null && model.TreatmentCodeIDs.Count > 0)
                {
                    // Add selected TreatmentCodes
                    foreach (var treatmentCodeID in model.TreatmentCodeIDs)
                    {
                        booking.BookingTreatmentCodes.Add(new BookingTreatmentCode
                        {
                            TreatmentCodeID = treatmentCodeID
                        });
                    }
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
            // Retrieve all bookings with related TreatmentCodes, Operation Theatre, and Anaesthesiologist
            var bookings = _Context.bookings
                .Include(b => b.BookingTreatmentCodes)
                    .ThenInclude(btc => btc.TreatmentCode)
                .Include(b => b.OperationTheatre)
                .Include(b => b.Anaesthesiologist)
                    .ThenInclude(a => a.ApplicationUser) // Ensure ApplicationUser is included if applicable
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
