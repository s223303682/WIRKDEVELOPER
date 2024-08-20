using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                return RedirectToAction("PrescriptionList");
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
            return RedirectToAction("PrescriptionList");

        }
        //public IActionResult BookingPatientList()
        //{
        //    IEnumerable<BookingPatient> list = _Context.bookingPatients;
        //    return View(list);
        //}
        //public IActionResult CreateBookingPatient()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateBookingPatient(BookingPatient bookingPatient)
        //{
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookingPatient.BookingPatientID = user;
            //int totalbcountry = 0;
            //int totalprovince = 0;
            //int totalcity = 0;
            //int totalsurbub = 0;
            //if (ModelState.IsValid)
            //{
            //    totalbcountry += Convert.ToInt32(bookingPatient.country);
            //    totalprovince += Convert.ToInt32(bookingPatient.province);
            //    totalcity += Convert.ToInt32(bookingPatient.City);
            //    totalsurbub += Convert.ToInt32(bookingPatient.Surbub);

            //    if (totalbcountry <= 1)
            //    {
            //        TempData["Results"] = "South Africa";
            //    }
            //    else if(totalprovince <= 2)
            //    {
            //        TempData["Results"] = "Eastern Cape";
            //        if (totalprovince <= 2)
            //        {
            //            if (totalcity <= 201)
            //            {
            //                TempData["Results"] = "Port Elizabeth";
            //                if (totalsurbub <= 101)
            //                {
            //                    TempData["Results"] = "summerstrand";
            //                }
            //                else if (totalsurbub <= 102)
            //                {
            //                    TempData["Results"] = "North end";
            //                }
            //            }
            //            else if (totalcity <= 202)
            //            {
            //                TempData["Results"] = "East London";

            //            }
            //        }
            //    }

            //    else if (totalprovince <= 3)
            //    {
            //        TempData["Results"] = "Cape Town";
                   

            //    }


        //        _Context.bookingPatients.Add(bookingPatient);
        //        _Context.SaveChanges();
        //        return RedirectToAction("BookingPatientList");
        //    }
        //    return View(bookingPatient);
        //}

        //public IActionResult updateBookingPatient(int? ID)
        //{
        //    if (ID == null || ID == 0)
        //    {
        //        return NotFound();
        //    }
        //    var list = _Context.bookingPatients.Find(ID);
        //    if (list == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(list);

        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult updateBookingPatient(BookingPatient bookingPatient)
        //{
        //    //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //bookSurgery.PatientID = user;
        //    _Context.bookingPatients.Update(bookingPatient);
        //    _Context.SaveChanges();
        //    return RedirectToAction("BookingPatientList");
        //}
        //public IActionResult DeleteBookingPatient(int? ID)
        //{
        //    var list = _Context.bookingPatients.Find(ID);
        //    if (list == null)
        //    {
        //        return NotFound();
        //    }
        //    _Context.bookingPatients.Remove(list);
        //    _Context.SaveChanges();
        //    return RedirectToAction("BookingPatientList");

        //}
        public IActionResult BookingList()
        {
            IEnumerable<Booking> list = _Context.bookings;
            return View(list);
        }
        public IActionResult CreateBooking()
        {
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", "PatientSurname");
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
                ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", "PatientSurname");
                return RedirectToAction("BookingList");
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
