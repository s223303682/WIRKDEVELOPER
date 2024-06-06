using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
		public IActionResult SearchPatient()
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
            var list = _Context.bookSurgeries.Find(ID);
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
        public IActionResult BookingList()
        {
            IEnumerable<BookSurgery> list = _Context.bookSurgeries;
            return View(list);
        }
        public IActionResult CreateBooking()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(BookSurgery bookSurgery)
        {
            if (ModelState.IsValid)
            {
                _Context.bookSurgeries.Add(bookSurgery);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(bookSurgery);
        }
        public IActionResult updateBooking(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.bookSurgeries.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateBooking(BookSurgery bookSurgery)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.bookSurgeries.Update(bookSurgery);
            _Context.SaveChanges();
            return RedirectToAction("BookingList");
        }
        public IActionResult DeleteBooking(int? ID)
        {
            var list = _Context.bookSurgeries.Find(ID);
            if (list == null)
            {
                return NotFound();
            }
            _Context.bookSurgeries.Remove(list);
            _Context.SaveChanges();
            return RedirectToAction("BookingList");

        }
    } 
}
