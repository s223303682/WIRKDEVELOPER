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
            IEnumerable<Prescription> list = _Context.prescriptions;
                 //.Include(a => a.PharmacyMedication);
            //.Include(a => a.Patient);
            return View(list);
        }
        public IActionResult CreatePrescription(int bookingID, string name, string gender, string email)
        {
            // Initialize a new prescription model with data from the booking
            var model = new Prescription
            {
                BookingID = bookingID.ToString(),
                Name = name,
                Gender = gender,
                Email = email,
                Date = DateTime.Now // Default to current date; adjust as needed
            };

            // Optionally, populate ViewBag or other data needed for the view
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescription(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _Context.prescriptions.Add(prescription);
                _Context.SaveChanges();
                return RedirectToAction("PrescriptionList");
            }

            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName", prescription.PrescriptionID);
            return View(prescription);
        }
        public IActionResult updatePrescription(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.prescriptions.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updatePrescription(Prescription prescription)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
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
