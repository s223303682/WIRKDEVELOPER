using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using WIRKDEVELOPER.Models;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Xml.Linq;
using WIRKDEVELOPER.Models.Account;

namespace WIRKDEVELOPER.Controllers
{
    public class AnaesthesiologistController : Controller
    {
        private readonly ApplicationDBContext _Context;

        public AnaesthesiologistController(ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult Anaesthesiologist()
        {
            return View();
        }
        public IActionResult IndexViewPatient(DateTime searchDate)
        {

            //var patient = _Context.admission.Where(e => e.Date == searchDate.Date).ToList();
            //return View(patient);
            var patient = _Context.addm.Include(a => a.Ward).Include(a => a.Bed).Include(a => a.Patient).Where(e => e.Date == searchDate.Date).ToList();
            return View(patient);
        }
        public IActionResult ViewPatientRec()
        {
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            ViewBag.getWard = new SelectList(_Context.ward, "WardID", "WardName");
            ViewBag.getBed = new SelectList(_Context.bed, "BedID", "BedNumber");

            return View();

        }
        public IActionResult IndexViewBookedPatients(DateTime searchDate)
        {
            var patient = _Context.addm.Include(a => a.Ward).Include(a => a.Bed).Include(a => a.Patient).Where(e => e.Date == searchDate.Date).ToList();
            return View(patient);
            

        }
        public IActionResult IndexAnVitals()
        {
            IEnumerable<AnVitals> objList = _Context.anvitals;
            return View(objList);
            //var patient = _Context.ViewRecords.Where(e => e.Date == searchDate.Date).ToList();
            //return View(patient);
        }
        public IActionResult AnVitals()
        {
            return View();
        }
        public IActionResult IndexAnAllergies()
        {
            IEnumerable<AnAllergies> objList = _Context.anallergies.Include(a => a.Active);
            return View(objList);

        }
        public IActionResult AnAllergies()
        {
            return View();
        }
        public IActionResult IndexCurrentMedication()
        {
            IEnumerable<AnCurrentMedication> objList = _Context.ancurrentmedication;
            return View(objList);

        }
        public IActionResult AnCurrentMedication()
        {
            return View();
        }
        public IActionResult IndexConditions()
        {
            IEnumerable<AnConditions> objList = _Context.anconditions;
            return View(objList);

        }
        public IActionResult AnConditions()
        {
            return View();
        }
        public IActionResult IndexOrders()
        {
            // Retrieve the list of orders from the database, including related entities if needed
            var orders = _Context.order
                .Include(o => o.Addm)                   // Include related Patient entity
                /*.Include(o => o.OrderItems) */                 // Include related OrderItems
                .Include(o => o.orderMedications)                  // Include related OrderItems
                .Include(o => o.PharmacyMedication)         // Include related PharmacyMedication entity
                //.ToListAsync()

             .Select(item => new OrderCreate
              {
                 Date = item.Date,
                 AddmID = item.AddmID,
                 Urgent = item.Urgent,
                 PharmacyMedicationID = item.PharmacyMedicationID,             
                 Status = "Ordered",
                 OrderItems = item.orderMedications.Select(m => new OrderItems
                 {
                      PharmacyMedicationID = m.PharmacyMedicationID,
                      Quantity = m.Quantity,
                      Instructions = m.Instructions
                  }).ToList()
              }).ToList();

            //return View(prescriptions);

            return View(orders);
        }
        public IActionResult CreateOrder(int AddmID, DateTime Date)
        {

            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            var medications = _Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList();

            // Serialize medications to JSON
            ViewBag.Medications = JsonConvert.SerializeObject(medications);

            var model = new OrderCreate
            {
                Date = Date,
                AddmID = AddmID,
                // Initialize other fields as needed
            };

            return View(model);

            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            //ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");

            //var model = new OrderCreate
            //{
            //    Medications = _Context.pharmacyMedications.ToList()
            //};
            //return View(model);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(OrderCreate viewModel)
        {
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Date = viewModel.Date,
                    AddmID = viewModel.AddmID,
                    Urgent = viewModel.Urgent,
                    Status = "Ordered",

                };
                _Context.order.Add(order);
                 _Context.SaveChanges();

                // Add the medications
                foreach (var medication in viewModel.OrderItems)
                {
                    var ordermedication = new OrderMedication
                    {
                        AnOrderID = order.AnOrderID, // Use the ID generated by the database
                        PharmacyMedicationID = medication.PharmacyMedicationID,
                        Quantity = medication.Quantity,
                        Instructions = medication.Instructions
                    };
                    _Context.ordermedication.Add(ordermedication);
                }

                _Context.SaveChanges();

                return RedirectToAction("IndexOrders");
            }
            // If the model is invalid, return the same view with validation errors
            var medications = _Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList();
            ViewBag.Medications = JsonConvert.SerializeObject(medications);
            return View(viewModel);


            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            //ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            
            //viewModel.Medications = _Context.pharmacyMedications.ToList();
            //return RedirectToAction("IndexOrders"); 
        }
        public async Task<IActionResult> EditOrder(int id)
        {
            var order = await _Context.order
                .Include(o => o.PharmacyMedication)
                .Include(o => o.Addm)
                .FirstOrDefaultAsync(o => o.AnOrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName", order.PharmacyMedicationID);
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", order.AddmID);

            var model = new OrderCreate
            {
                Date = (DateTime)order.Date,
                AddmID = order.AddmID,
                Urgent = order.Urgent,
                OrderItems = new List<OrderItems>
                {
                    new OrderItems
                    {
                         PharmacyMedicationID = order.PharmacyMedicationID,
                        Quantity = (int)order.Quantity,
                        Instructions = order.Instructions
                    }
                },
                Medications = _Context.pharmacyMedications.ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(int id, OrderCreate viewModel)
        {
            if (id != viewModel.AnOrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _Context.order.FindAsync(id);
                    if (order == null)
                    {
                        return NotFound();
                    }


                    order.Date = viewModel.Date;
                    order.AddmID = viewModel.AddmID;
                    order.Urgent = viewModel.Urgent;
                    order.PharmacyMedicationID = viewModel.OrderItems[0].PharmacyMedicationID;
                    order.Quantity = viewModel.OrderItems[0].Quantity;
                    order.Instructions = viewModel.OrderItems[0].Instructions;

                    _Context.Update(order);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_Context.order.Any(e => e.AnOrderID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexOrders");
            }

            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName", viewModel.OrderItems[0].PharmacyMedicationID);
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", viewModel.AddmID);
            viewModel.Medications = _Context.pharmacyMedications.ToList();

            return View(viewModel);
        }


        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _Context.order
                .Include(o => o.PharmacyMedication)
                .Include(o => o.Addm)
                .FirstOrDefaultAsync(o => o.AnOrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _Context.order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _Context.order.Remove(order);
            await _Context.SaveChangesAsync();
            return RedirectToAction("IndexOrders");
        }






        //public IActionResult IndexOrder()
        //{
        //    IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication).Include(a => a.Addm);
        //    return View(objList);

        //}
        //public IActionResult Order()
        //{

        //    ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
        //    ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");

        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Order(Order order)
        //{

        //    _Context.order.Add(order);
        //    ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
        //    ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");

        //    _Context.SaveChanges();


        //    return RedirectToAction("IndexOrder");


        //}
        //public IActionResult NewUpdateOrder(int? ID)
        //{


        //    if (ID == null || ID == 0)
        //    {
        //        return NotFound();
        //    }
        //    var objList = _Context.order.Find(ID);
        //    if (objList == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
        //    ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
        //    return View(objList);

        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult NewUpdateOrder(Order order)
        //{
        //    _Context.order.Update(order);
        //    ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
        //    ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
        //    _Context.SaveChanges();
        //    return RedirectToAction("IndexOrder");

        //}
        //public IActionResult DeleteOrder(int? ID)
        //{
        //    var obj = _Context.order.Find(ID);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _Context.order.Remove(obj);
        //    _Context.SaveChanges();
        //    return RedirectToAction("IndexOrder");
        //}
        public IActionResult IndexVitalRanges()
        {
            IEnumerable<VitalRanges> objList = _Context.vitalranges;
            return View(objList);

        }
        public IActionResult VitalRanges()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VitalRanges(VitalRanges vitalranges)
        {
            if (ModelState.IsValid)
            {
                _Context.vitalranges.Update(vitalranges);
                _Context.SaveChanges();
                return RedirectToAction("IndexVitalRanges");
            }
            return View(vitalranges);

        }
        public IActionResult UpdateVitalRanges(int? ID)
        {

            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.vitalranges.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

            //if (VitalRangeID == null || VitalRangeID == 0)
            //{
            //    return NotFound();
            //}
            //var obj = _Context.vitalranges.Find(VitalRangeID);

            //if (VitalRangeID == null)
            //{
            //    return NotFound();
            //}
            //return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateVitalRanges(int ID, VitalRanges vitalRanges)
        {


            _Context.vitalranges.Update(vitalRanges);
            _Context.SaveChanges();
            return RedirectToAction("IndexVitalRanges");

            //if (ModelState.IsValid)
            //{
            //    _Context.vitalranges.Update(vitalRanges);
            //    _Context.SaveChanges();
            //    return RedirectToAction("IndexVitalRanges");
            //}
            //return View(vitalRanges);
        }
        public IActionResult DeleteVitalRanges(int? ID)
        {

            var obj = _Context.vitalranges.Find(ID);

            if (obj == null)
            {
                return NotFound();
            }
            _Context.vitalranges.Remove(obj);
            _Context.SaveChanges();
            return RedirectToAction("IndexVitalRanges");

            //var obj = _Context.vitalranges.Find(VitalRangeID);

            //if (obj == null)
            //{
            //    return NotFound();
            //}
            //_Context.vitalranges.Remove(obj);
            //_Context.SaveChanges();
            //return RedirectToAction("IndexVitalRanges");
        }

        //public PatientController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public IActionResult SearchPatient(string patientName)
        {


      
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication).Include(a => a.Addm);
            return View(objList);
            
        }
       
        public IActionResult CreateNoteForOrder(int ID)
        {

            if(ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.order.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            return View(objList);
            //var order = _Context.order.Find(orderId);
            //if (order == null)
            //{
            //    return NotFound();
            //}
            //ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");

            //ViewBag.getPatient = order.AddmID;

            //var note = new Order
            //{
            //    AnOrderID = orderId
            //};

            //    return View(note);
        }


        [HttpPost]
        public IActionResult CreateNoteForOrder(Order note)
        {
            //_Context.order.Update(note);
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            //ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            //_Context.SaveChanges();
            //return RedirectToAction("SearchPatient");

            if (ModelState.IsValid)
             {
                 _Context.order.Update(note);
                 _Context.SaveChanges();
                   return RedirectToAction("SearchPatient");
            }

             ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
             ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
             return View(note); // Return the view with validation errors if any

            //note.Date = DateTime.Now;
            //_Context.order.Add(note);
            //ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName");
            //_Context.SaveChanges();

            //var order = _Context.order.Find(note.AnOrderID);
            //return RedirectToAction("SearchPatient", new { patientName = order.AddmID });


        }

        public IActionResult IndexVitalHistory()
        {
            IEnumerable<AnVitals> objList = _Context.anvitals;
            return View(objList);
        }
        public IActionResult VitalHistory()
        {
            return View();
        }
        //public IActionResult Indexclassadd()
        //{
        //    IEnumerable<ClassAdd> objList = _Context.classadd;
        //    return View(objList);
        //}
        //public IActionResult ClassAdd()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ClassAdd(ClassAdd classAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _Context.classadd.Add(classAdd);
        //        _Context.SaveChanges();
        //        return RedirectToAction("Indexclassadd");
        //    }
        //    return View(classAdd);

        //}
        //public IActionResult Updateclassadd(int? AdmissionID)
        //{
        //    if (AdmissionID == null || AdmissionID == 0)
        //    {
        //        return NotFound();
        //    }
        //    var obj = _Context.classadd.Find(AdmissionID);

        //    if (AdmissionID == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Updateclassadd(ClassAdd classAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _Context.classadd.Update(classAdd);
        //        _Context.SaveChanges();
        //        return RedirectToAction("Indexclassadd");
        //    }
        //    return View(classAdd);
        //}
        //public IActionResult DeleteClassAdd(int? AdmissionID)
        //{
        //    var obj = _Context.classadd.Find(AdmissionID);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _Context.classadd.Remove(obj);
        //    _Context.SaveChanges();
        //    return RedirectToAction("Indexclassadd");
    }
}               //}











