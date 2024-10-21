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
using DinkToPdf;
using DinkToPdf.Contracts;
using WIRKDEVELOPER.Models.PatientHistory;
using System.Text;
using System.Linq;

namespace WIRKDEVELOPER.Controllers
{
    public class SurgeonController: Controller
    {
        private readonly ApplicationDBContext _Context;
        private readonly EmailService _emailService;
        private readonly IConverter _converter;
        public SurgeonController(ApplicationDBContext applicationDBContext, EmailService emailService, IConverter converter)
        {
            _Context = applicationDBContext;
            _emailService = emailService;
            _converter = converter;
        }
		public IActionResult AdmittedPatients()
		{
			var patients = _Context.addm
				.Include(p => p.Patient)
				.Include(p => p.Ward)
				.Include(p => p.Bed)
				.Include(p => p.ChronicMedication)
				.Include(p => p.AnAllergies)
				.Include(p => p.AnCurrentMedication)
				.Include(p => p.AnConditions)
				.ToList();

			return View(patients);
		}

		public IActionResult GenerateReport()
        {
            return View();
        }
        public IActionResult ReportView()
        {
            return View();
        }

        // Action to generate the report view
        [HttpPost]
        public IActionResult GenerateSurgeryReport(DateTime startDate, DateTime endDate)
        {
            var bookings = _Context.bookings
                .Where(b => b.Date >= startDate && b.Date <= endDate)
                .Include(b => b.BookingTreatmentCodes)
                .ThenInclude(tc => tc.TreatmentCode)
                .ToList();

            var treatmentCodeSummary = bookings
                .SelectMany(b => b.BookingTreatmentCodes)
                .GroupBy(tc => tc.TreatmentCode.ICDCODE)
                .Select(g => new TreatmentCodeSummary
                {
                    TreatmentCode = g.Key,
                    TotalSurgeries = g.Count()
                })
                .ToList();

            // Create a ViewModel to pass the data to the view
            var reportViewModel = new SurgeryReportViewModel
            {
                Bookings = bookings,
                TreatmentCodeSummary = treatmentCodeSummary,
                StartDate = startDate,
                EndDate = endDate,
                ReportGeneratedDate = DateTime.Now
            };

            return View("ReportView", reportViewModel);
        }

        // Action to generate the PDF report
        public IActionResult DownloadSurgeryReport(DateTime startDate, DateTime endDate)
        {
            // Logic to generate PDF similar to the GenerateSurgeryReport action
            var bookings = _Context.bookings
                .Where(b => b.Date >= startDate && b.Date <= endDate)
                .Include(b => b.BookingTreatmentCodes)
                .ThenInclude(tc => tc.TreatmentCode)
                .ToList();

            var treatmentCodeSummary = bookings
                .SelectMany(b => b.BookingTreatmentCodes)
                .GroupBy(tc => tc.TreatmentCode.ICDCODE)
                .Select(g => new TreatmentCodeSummary
                {
                    TreatmentCode = g.Key,
                    TotalSurgeries = g.Count()
                })
                .ToList();

            var pdfDoc = new HtmlToPdfDocument
            {
                GlobalSettings =
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
        },
                Objects =
        {
            new ObjectSettings()
            {
                HtmlContent = GenerateHtmlContent(bookings, treatmentCodeSummary, startDate, endDate),
            }
        }
            };

            var pdf = _converter.Convert(pdfDoc);
            return File(pdf, "application/pdf", "SurgeryReport.pdf");
        }


        private string GenerateHtmlContent(List<Booking> bookings, List<TreatmentCodeSummary> treatmentCodeSummary, DateTime startDate, DateTime endDate)
        {
            var htmlContent = new StringBuilder();
            var reportGeneratedDate = DateTime.Now.ToString("d MMMM yyyy");

            // Add report header
            htmlContent.Append($@"
        <h1>SURGERY REPORT</h1>
        <h2>Dr Jacob Moloi</h2>
        <h3>Date Range: {startDate.ToString("d MMMM yyyy")} – {endDate.ToString("d MMMM yyyy")} | Report Generated: {reportGeneratedDate}</h3>
        <table style='width:100%; border-collapse: collapse;'>
            <thead>
                <tr>
                    <th style='border: 1px solid black; padding: 8px;'>DATE</th>
                    <th style='border: 1px solid black; padding: 8px;'>PATIENT</th>
                    <th style='border: 1px solid black; padding: 8px;'>TREATMENT CODE(S)</th>
                </tr>
            </thead>
            <tbody>");

            foreach (var booking in bookings)
            {
                var treatmentCodes = booking.BookingTreatmentCodes.Select(tc => tc.TreatmentCode.ICDCODE).ToArray();
                htmlContent.Append($@"
            <tr>
                <td style='border: 1px solid black; padding: 8px;'>{booking.Date.ToShortDateString()}</td>
                <td style='border: 1px solid black; padding: 8px;'>{booking.Name} {booking.Surname}</td>
                <td style='border: 1px solid black; padding: 8px;'>{string.Join(", ", treatmentCodes)}</td>
            </tr>");
            }

            htmlContent.Append($@"
            </tbody>
        </table>
        <h3 style='margin-top: 20px;'>TOTAL PATIENTS: {bookings.Count}</h3>
        <h2>SUMMARY PER TREATMENT CODE:</h2>
        <table style='width:100%; border-collapse: collapse;'>
            <thead>
                <tr>
                    <th style='border: 1px solid black; padding: 8px;'>TREATMENT CODE</th>
                    <th style='border: 1px solid black; padding: 8px;'>TOTAL SURGERIES</th>
                </tr>
            </thead>
            <tbody>");

            foreach (var summary in treatmentCodeSummary)
            {
                htmlContent.Append($@"
            <tr>
                <td style='border: 1px solid black; padding: 8px;'>{summary.TreatmentCode}</td>
                <td style='border: 1px solid black; padding: 8px;'>{summary.TotalSurgeries}</td>
            </tr>");
            }

            htmlContent.Append($@"
            </tbody>
        </table>");

            return htmlContent.ToString();
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
        // GET: CreatePrescription
        public IActionResult CreatePrescription(int bookingID, string name, string surname, string idNumber, string email, string gender)
        {
            var viewModel = new PrescriptionViewModel
            {
                Name = name,
                Surname = surname,
                IDNumber = idNumber,
                Email = email,
                Gender = gender,
                Date = DateTime.Now,
                Medications = new List<PrescriptionMedicationViewModel>(),
                HasAlerts = false // Initialize to false
            };

            var medications = _Context.pharmacyMedications
                .Select(pm => new SelectListItem
                {
                    Value = pm.PharmacyMedicationName, // Assuming this is the value you want to use
                    Text = pm.PharmacyMedicationName
                }).ToList();

            ViewBag.Medications = medications;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(PrescriptionViewModel model)
        {
            // Check for alerts
            var alertViewModel = new AlertViewModel
            {
                HasAlerts = false,
                IgnoreReason = model.IgnoreReason,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Gender = model.Gender,
                IDNumber = model.IDNumber,
                Date = model.Date,
                Prescriber = model.Prescriber,
                Status = model.Status,
                Urgent = model.Urgent,
                Medications = model.Medications
            };

            // Fetch current medications and allergies for the patient
            var currentMedications = await _Context.addm
                .Where(a => a.Patient.PatientIDNO == model.IDNumber)
                .Select(a => a.AnCurrentMedication.ChronicMedication.MedicationActive.Active.ActiveName)
                .ToListAsync();

            var patientAllergies = await _Context.addm
                .Where(a => a.Patient.PatientIDNO == model.IDNumber)
                .Select(a => a.AnAllergies.Active.ActiveName)
                .ToListAsync();

            // Check for allergy alerts and interactions
            foreach (var medication in model.Medications)
            {
                var prescribedActiveIngredients = await _Context.PharmacyMedicationIngredients
                    .Where(pm => pm.PharmacyMedication.PharmacyMedicationName == medication.PharmacyMedicationName)
                    .Select(pm => pm.Active.ActiveName)
                    .ToListAsync();

                // Check for allergy matches
                //if (patientAllergies != null && prescribedActiveIngredients.Any(ai => patientAllergies.Contains(ai)))
                //{
                //    alertViewModel.HasAlerts = true;
                //    break;
                //}

                // Check for drug interactions
                if (currentMedications.Any(cm =>
                    (cm == "Carbimazole" && prescribedActiveIngredients.Contains("Doxazosin")) ||
                    (cm == "Doxazosin" && prescribedActiveIngredients.Contains("Doxylamine Succinate"))))
                {
                    alertViewModel.HasAlerts = true;
                    break;
                }
            }

            // If there are alerts, ensure IgnoreReason is provided
            if (alertViewModel.HasAlerts)
            {
                if (string.IsNullOrWhiteSpace(model.IgnoreReason))
                {
                    ModelState.AddModelError("IgnoreReason", "You must provide a reason for ignoring the alert.");
                    return View("Alert", alertViewModel); // Return the alert view with medication details and alert info
                }

                // Proceed with creating prescription with the alert and reason
                var prescriptionWithAlert = new Prescription
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    IDNumber = model.IDNumber,
                    Email = model.Email,
                    Gender = model.Gender,
                    Date = DateTime.Now,
                    Prescriber = model.Prescriber,
                    Urgent = model.Urgent,
                    Status = "Pending with Alert",
                    IgnoreReason = model.IgnoreReason,
                    PrescriptionMedications = model.Medications.Select(m => new PrescriptionMedication
                    {
                        PharmacyMedicationID = GetPharmacyMedicationId(m.PharmacyMedicationName), // Ensure this method returns a valid ID
                        Quantity = m.Quantity,
                        Instructions = m.Instructions
                    }).ToList()
                };

                _Context.prescriptions.Add(prescriptionWithAlert);
                await _Context.SaveChangesAsync();

                return RedirectToAction("PrescriptionList");
            }

            // If no alerts, proceed with normal prescription creation
            var prescriptionWithoutAlerts = new Prescription
            {
                Name = model.Name,
                Surname = model.Surname,
                IDNumber = model.IDNumber,
                Email = model.Email,
                Gender = model.Gender,
                Date = DateTime.Now,
                Prescriber = model.Prescriber,
                Urgent = model.Urgent,
                Status = "Created",
                PrescriptionMedications = model.Medications.Select(m => new PrescriptionMedication
                {
                    PharmacyMedicationID = GetPharmacyMedicationId(m.PharmacyMedicationName), // Ensure this method returns a valid ID
                    Quantity = m.Quantity,
                    Instructions = m.Instructions
                }).ToList()
            };

            _Context.prescriptions.Add(prescriptionWithoutAlerts);
            await _Context.SaveChangesAsync();

            return RedirectToAction("PrescriptionList");
        }

        private int GetPharmacyMedicationId(string pharmacyMedicationName)
        {
            var medication = _Context.pharmacyMedications
                .FirstOrDefault(pm => pm.PharmacyMedicationName == pharmacyMedicationName);

            if (medication != null)
            {
                return medication.PharmacyMedicationID; // Ensure this property exists
            }
            throw new ArgumentException("Medication not found");
        }
        public async Task<IActionResult> PrescriptionList()
        {
            var prescriptions = await _Context.prescriptions
                .Include(p => p.PrescriptionMedications) // Include medications
                .Select(p => new PrescriptionViewModel
                {
                    PrescriptionID = p.PrescriptionID,
                    Name = p.Name,
                    Surname = p.Surname,
                    IDNumber = p.IDNumber,
                    Email = p.Email,
                    Gender = p.Gender,
                    Date = p.Date,
                    Status = p.Status,
                    IgnoreReason = p.IgnoreReason,
                    Urgent = p.Urgent,
                    Medications = p.PrescriptionMedications.Select(pm => new PrescriptionMedicationViewModel
                    {
                        PharmacyMedicationName = pm.PharmacyMedication.PharmacyMedicationName,
                        Quantity = pm.Quantity,
                        Instructions = pm.Instructions
                    }).ToList()
                }).ToListAsync();

            return View(prescriptions);
        }

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
        // Action to display the list of bookings
        public IActionResult BookingPatientList()
        {
            // Fetch bookings and include related details
            var bookings = _Context.bookingNewPatients
                .Include(b => b.OperationTheatre)
                .Include(b => b.Anaesthesiologist)
                    .ThenInclude(a => a.ApplicationUser)
                .Include(b => b.TreatmentCodes)
                    .ThenInclude(tc => tc.TreatmentCode)
                .Select(b => new BookingListViewModel
                {
                    BookingNewPatientID = b.BookingNewPatientID,
                    BookingNewPatientName = b.BookingNewPatientName,
                    BookingNewPatientSurname = b.BookingNewPatientSurname,
                    BookingNewPatientIDNUmber = b.BookingNewPatientIDNUmber,
                    Email = b.Email,
                    Date = b.Date,
                    Province = b.Province,
                    City = b.City,
                    Suburb = b.Suburb,
                    Zip = b.Zip,
                    OperationTheatreName = b.OperationTheatre.OperationTheatreName,
                    AnaesthesiologistName = b.Anaesthesiologist.ApplicationUser.FirstName + " " + b.Anaesthesiologist.ApplicationUser.LastName,
                    TreatmentCodes = b.TreatmentCodes.Select(tc => tc.TreatmentCode.ICDCODE).ToList()
                })
                .ToList();

            return View(bookings);
        }

        // Action to create a new booking (GET)
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

			ViewBag.getAnaesthesiologist = anaesthesiologists;
			return View();
		}

		// Action to create a new booking (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateBookingPatient(BookingNewPatient bookingNewPatient, List<int> selectedTreatmentCodeIDs)
		{
			if (ModelState.IsValid)
			{
				// Add the new booking to the database
				_Context.bookingNewPatients.Add(bookingNewPatient);

				// Add selected treatment codes to the booking
				foreach (var codeID in selectedTreatmentCodeIDs)
				{
					var treatmentCode = _Context.treatmentCodes.FirstOrDefault(tc => tc.TreatmentCodeID == codeID);
					if (treatmentCode != null)
					{
						bookingNewPatient.TreatmentCodes.Add(new BookingPatientTreatmentCode
						{
							BookingNewPatientID = bookingNewPatient.BookingNewPatientID,
							TreatmentCodeID = codeID
						});
					}
				}

				// Save changes to the database
				await _Context.SaveChangesAsync();
                // Send confirmation email
                string subject = "Your Booking Confirmation";
                string body = $@"
<p>Dear {bookingNewPatient.BookingNewPatientName} {bookingNewPatient.BookingNewPatientSurname},</p>
<p>We are pleased to confirm your booking for the following details:</p>
<ul>
    <li><strong>Date:</strong> {bookingNewPatient.Date.ToString("dddd, MMMM dd, yyyy")}</li>
    <li><strong>Time:</strong> {bookingNewPatient.Time.Hours:D2}:{bookingNewPatient.Time.Minutes:D2} {(bookingNewPatient.Time.Hours >= 12 ? "PM" : "AM")}</li>
</ul>
<p>Please ensure to arrive at least 15 minutes before your scheduled time.</p>
<p>If you have any questions or need to reschedule, feel free to contact us at qumbucommunityhealth@gmail.com.</p>
<p>Thank you for choosing us, and we look forward to serving you.</p>
<p>Best regards,<br/>
st Marys Health care Team</p>
";

                await _emailService.SendEmailAsync(bookingNewPatient.Email, subject, body);
                return RedirectToAction("BookingPatientList"); // Redirect to the list of bookings

            }

            // If there are validation errors, re-populate ViewBag data and return to the view
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
			ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");

			var anaesthesiologists = _Context.Anaesthesiologists
				.Select(a => new SelectListItem
				{
					Value = a.UserId.ToString(),
					Text = $"{a.ApplicationUser.FirstName} {a.ApplicationUser.LastName}"
				}).ToList();

			ViewBag.getAnaesthesiologist = anaesthesiologists;

			return View(bookingNewPatient);
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
        // Action to delete a booking (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            // Find the booking by ID
            var booking = await _Context.bookingNewPatients
                .Include(b => b.TreatmentCodes) // Include treatment codes if needed for deletion
                .FirstOrDefaultAsync(b => b.BookingNewPatientID == id);

            if (booking != null)
            {
                // Remove the booking and any related treatment codes
                _Context.bookingNewPatients.Remove(booking);
                await _Context.SaveChangesAsync();

                // Optionally, you can send a confirmation email or handle other logic
            }

            // Redirect to the booking list after deletion
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
        public async Task<IActionResult> CreateBooking(BookingViewModel model)
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
                // Send confirmation email
                string subject = "Your Booking Confirmation";
                string body = $@"
<p>Dear {model.Name} {model.Surname},</p>
<p>We are pleased to confirm your booking for the following details:</p>
<ul>
    <li><strong>Date:</strong> {model.Date.ToString("dddd, MMMM dd, yyyy")}</li>
    <li><strong>Time:</strong> {model.Time.Hours:D2}:{model.Time.Minutes:D2} {(model.Time.Hours >= 12 ? "PM" : "AM")}</li>
</ul>
<p>Please ensure to arrive at least 15 minutes before your scheduled time.</p>
<p>If you have any questions or need to reschedule, feel free to contact us at qumbucommunityhealth@gmail.com.</p>
<p>Thank you for choosing us, and we look forward to serving you.</p>
<p>Best regards,<br/>
St Marys Health Care Team</p>
";

                await _emailService.SendEmailAsync(model.Email, subject, body);
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
