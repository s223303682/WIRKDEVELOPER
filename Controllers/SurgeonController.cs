using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Core.Types;
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
            return View(list);
        }
        public IActionResult CreatePrescription()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescription(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _Context.prescriptions.Add(prescription);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
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
            return RedirectToAction("PrescitionList");

        }
        public IActionResult BookingPatientList()
        {
            IEnumerable<BookingPatient> list = _Context.bookingPatients;
            return View(list);
        }
        public IActionResult CreateBookingPatient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBookingPatient(BookingPatient bookingPatient)
        {
            if (ModelState.IsValid)
            {
                _Context.bookingPatients.Add(bookingPatient);
                _Context.SaveChanges();
                return RedirectToAction("BookingPatientList");
            }
            return View(bookingPatient);
        }

        public IActionResult updateBookingPatient(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.bookingPatients.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateBookingPatient(BookingPatient bookingPatient)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.bookingPatients.Update(bookingPatient);
            _Context.SaveChanges();
            return RedirectToAction("BookingPatientList");
        }
        public IActionResult DeleteBookingPatient(int? ID)
        {
            var list = _Context.bookingPatients.Find(ID);
            if (list == null)
            {
                return NotFound();
            }
            _Context.bookingPatients.Remove(list);
            _Context.SaveChanges();
            return RedirectToAction("BookingPatientList");

        }
        public IActionResult BookingList()
        {
            IEnumerable<Booking> list = _Context.bookings;
            return View(list);
        }
        public IActionResult CreateBooking()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _Context.bookings.Add(booking);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
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
