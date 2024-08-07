using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;

namespace WIRKDEVELOPER.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly ApplicationDBContext _Context;

        public PharmacistController(ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult CreateMedication()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMedication(Medication medication)
        {
            if (ModelState.IsValid)
            {
                _Context.medications.Add(medication);
                _Context.SaveChanges();
                return RedirectToAction("MedicationList");
            }
            return View(medication);
           
        }
        public IActionResult MedicationList()
        {
            IEnumerable<Medication> list = _Context.medications;
            return View(list);
        }
        public IActionResult Createstock()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Createstock(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _Context.stocks.Add(stock);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(stock);
        }
        public IActionResult StockList()
        {
            IEnumerable<Stock> list = _Context.stocks;
            return View(list);
        }
        public IActionResult updateStock(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var list = _Context.stocks.Find(ID);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateStock( Stock stock)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.stocks.Update(stock);
            _Context.SaveChanges();
            return RedirectToAction("StockList");
        }
        public async Task<IActionResult> AcceptPrescription(int? ID)
        {
            var prescriptions = _Context.prescriptions.Find(ID);
            if (prescriptions != null)
            {
                prescriptions.status = "Accepted";
                _Context.prescriptions.Update(prescriptions);
                await _Context.SaveChangesAsync();
                TempData["Info"] = "Dispsed";
                //var patient = _Context.Users.Where(a => a.Id == prescriptions.PatientID).FirstOrDefault();
                
            }
            ViewData["PatientID"] = new SelectList(_Context.Users, "Id", "Id", prescriptions.PatientID);
            return View(prescriptions);
        }
        public IActionResult PharmPrescriptionList()
        {
            IEnumerable<Prescription> list = _Context.prescriptions;
            return View(list);
        }
        public async Task<IActionResult> AcceptOrder(int? ID)
        {
            var order = _Context.prescriptions.Find(ID);
            if (order != null)
            {
                order.status = "Accepted";
                _Context.prescriptions.Update(order);
                await _Context.SaveChangesAsync();
                TempData["Info"] = "Dispsed";
                //var patient = _Context.Users.Where(a => a.Id == prescriptions.PatientID).FirstOrDefault();

            }
           //ViewData["PatientID"] = new SelectList(_Context.Users, "Id", "Id", orders.PatientID);
            return View(order);
        }
        public IActionResult PharmIndexOrder()
        {
            IEnumerable<Order> objList = _Context.order;
            return View(objList);

        }
        public IActionResult IndexMedication()
        {
            IEnumerable<PharmacyMedication> objList = _Context.pharmacyMedications;
            return View(objList);

        }
        public IActionResult AddMedication()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedication(PharmacyMedication pharmacyMedications)
        {
            if (ModelState.IsValid)
            {
                _Context.pharmacyMedications.Update(pharmacyMedications);
                _Context.SaveChanges();
                return RedirectToAction("IndexMedication");
            }
            return View(pharmacyMedications);

        }
    }
}
