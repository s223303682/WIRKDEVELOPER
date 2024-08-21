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
            IEnumerable<Prescription> list = _Context.prescriptions
                 .Include(a => a.PharmacyMedication);
            //.Include(a => a.admission);
            return View(list);
        }
        public IActionResult CreatePrescription()
        {
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            // ViewBag.getPatient = new SelectList(_Context.admission, "PatientID", "PatientName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescription(Prescription prescription)
        {
            _Context.prescriptions.Add(prescription);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            // ViewBag.getPatient = new SelectList(_Context.admission, "PatientID", "PatientName");
            _Context.SaveChanges();
            return RedirectToAction("PrescriptionList");
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
            IEnumerable<BookingNew> list = _Context.bookingnew
                .Include(a => a.OperationTheatre)
                .Include(s => s.Anaestesiologist)
                .Include(s => s.treatmentCode);


            return View(list);
        }
        public IActionResult CreateBookingPatient()
        {
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "FirstName", "LastName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBookingPatient(BookingNew bookingNew)
        {

            _Context.bookingnew.Add(bookingNew);
            ViewBag.getOperationTheatre = new SelectList(_Context.operationTheatres, "operationTheatresID", "OperationTheatreName");
            ViewBag.getTreatmentCode = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "FirstName", "LastName");
            _Context.SaveChanges();
            return RedirectToAction("BookingPatientList");
        }
        //return View(bookingNew);



        public IActionResult updateBookingPatient(int? ID)
    {
        if (ID == null || ID == 0)
        {
            return NotFound();
        }
        var list = _Context.bookingnew.Find(ID);
        if (list == null)
        {
            return NotFound();
        }

        return View(list);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult updateBookingPatient(BookingNew bookingNew)
    {
        //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //bookSurgery.PatientID = user;
        _Context.bookingnew.Update(bookingNew);
        _Context.SaveChanges();
        return RedirectToAction("BookingPatientList");
    }
    public IActionResult DeleteBookingPatient(int? ID)
    {
        var list = _Context.bookingnew.Find(ID);
        if (list == null)
        {
            return NotFound();
        }
        _Context.bookingnew.Remove(list);
        _Context.SaveChanges();
        return RedirectToAction("BookingPatientList");

    }
        public IActionResult BookingList()
        {

            IEnumerable<Booking> list = _Context.bookings
                 .Include(a => a.OperationTheatre)
                .Include(s => s.Anaestesiologist)
                .Include(s => s.treatmentCode);
            return View(list);
        }
        public IActionResult CreateBooking()
        {
            ViewBag.getgetOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCodeID = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "FirstName", "LastName");
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", "PatientSurname");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking)
        {


            _Context.bookings.Add(booking);
            ViewBag.getgetOperationTheatre = new SelectList(_Context.operationTheatres, "OperationTheatreID", "OperationTheatreName");
            ViewBag.getTreatmentCodeID = new SelectList(_Context.treatmentCodes, "TreatmentCodeID", "ICDCODE");
            ViewBag.getAnaesthesiologist = new SelectList(_Context.Anaesthesiologists, "UserId", "FirstName", "LastName");
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", "PatientSurname");
            _Context.SaveChanges();

            return RedirectToAction("BookingList");

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
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
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
