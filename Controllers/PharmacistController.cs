using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public IActionResult UpdatePrescription(int id)
        {
            var prescription = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var model = new PrescriptionViewModel
            {
                PrescriptionViewModelID = prescription.PrescriptionID,
                Name = prescription.Name,
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

            ViewBag.Medications = JsonConvert.SerializeObject(_Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList());

            return View(model);
        }


        // POST: Prescription/Update/5
        [HttpPost]
        public IActionResult UpdatePrescription(PrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Log or inspect the model.PrescriptionID to verify it's not zero
                var prescription = _Context.prescriptions
                    .Include(p => p.PrescriptionMedications)
                    .FirstOrDefault(p => p.PrescriptionID == model.PrescriptionViewModelID);

                if (prescription == null)
                {
                    return NotFound();
                }

                // Update prescription details
                prescription.Name = model.Name;
                prescription.Gender = model.Gender;
                prescription.Email = model.Email;
                prescription.Date = model.Date;
                prescription.Prescriber = model.Prescriber;
                prescription.Urgent = model.Urgent;
                prescription.Status = model.Status;

                // Remove existing medications
                _Context.prescriptionMedications.RemoveRange(prescription.PrescriptionMedications);

                // Add updated medications
                foreach (var medication in model.Medications)
                {
                    var prescriptionMedication = new PrescriptionMedication
                    {
                        PrescriptionID = prescription.PrescriptionID,
                        PharmacyMedicationID = medication.PharmacyMedicationID,
                        Quantity = medication.Quantity,
                        Instructions = medication.Instructions
                    };
                    _Context.prescriptionMedications.Add(prescriptionMedication);
                }

                _Context.SaveChanges();

                return RedirectToAction("PharmPrescriptionList");
            }

            // If the model is invalid, reload medications and return the view
            var medications = _Context.pharmacyMedications
                .Select(pm => new { pm.PharmacyMedicationID, pm.PharmacyMedicationName })
                .ToList();
            ViewBag.Medications = JsonConvert.SerializeObject(medications);
            return View(model);
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
   
        //public async Task<IActionResult> AcceptPrescription(int? ID)
        //{
        //    var prescriptions = _Context.prescriptions.Find(ID);
        //    if (prescriptions != null)
        //    {
        //        prescriptions.status = "Accepted";
        //        _Context.prescriptions.Update(prescriptions);
        //        await _Context.SaveChangesAsync();
        //        TempData["Info"] = "Dispsed";
        //        //var patient = _Context.Users.Where(a => a.Id == prescriptions.PatientID).FirstOrDefault();
                
        //    }
        //    ViewData["PatientID"] = new SelectList(_Context.Users, "Id", "Id", prescriptions.PatientID);
        //    return View(prescriptions);
        //}
        public IActionResult PharmPrescriptionList()
        {
            var prescriptions = _Context.prescriptions
               .Include(p => p.PrescriptionMedications)
                   .ThenInclude(pm => pm.PharmacyMedication) // Include related PharmacyMedication data
               .Select(p => new PrescriptionViewModel
               {
                   Name = p.Name,
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
        public IActionResult AllPrescriptionList()
        {
            var prescriptions = _Context.prescriptions
               .Include(p => p.PrescriptionMedications)
                   .ThenInclude(pm => pm.PharmacyMedication) // Include related PharmacyMedication data
               .Select(p => new PrescriptionViewModel
               {
                   Name = p.Name,
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
        //public async Task<IActionResult> AcceptOrder(int? ID)
        //{
        //    var order = _Context.order.Find(ID);
        //    if (order != null)
        //    {
        //        order.Status = "Ordered";
        //        _Context.order.Update(order);
        //        await _Context.SaveChangesAsync();
        //        TempData["Ordered"] = "Dispensed";
        //        var patient = _Context.Users.Where(a => a.Id == order.Patient).FirstOrDefault();

        //    }
        //    ViewData["Patient"] = new SelectList(_Context.Users, "Id", "Id", order.Patient);
        //    return View(order);
        //}
        public IActionResult PharmIndexOrder()
        {
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication).Include(a => a.Addm);
            return View(objList);
            //IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication);
            //return View(objList);
            //IEnumerable<Order> objList = _Context.order;
            //return View(objList);

        }
        public IActionResult AllIndexOrder()
        {
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication).Include(a => a.Addm);
            return View(objList);
            //IEnumerable<Order> objList = _Context.order;
            //return View(objList);

        }
        public IActionResult IndexMedication()
        {
            IEnumerable<PharmacyMedication> objList = _Context.pharmacyMedications
                .Include(a => a.Ingredients)
                .ThenInclude(a => a.Active)
                .Include(a => a.DosageForm)
                .Include(s => s.Schedule);
               // .Include(a => a.Active);
            return View(objList);

        }
        public IActionResult AddMedication()
        {
            ViewBag.getDosage = new SelectList(_Context.dosageForms, "DosageFormID", "DosageFormName");
            ViewBag.getActive = new SelectList(_Context.active, "ActiveID", "ActiveName");
            ViewBag.getSchedule = new SelectList(_Context.schedules, "ScheduleId", "ScheduleName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMedication(PharmacyMedication medication, List<int> selectedIngredients, List<string> ingredientStrengths)
        {

            // Add the medication first
            _Context.pharmacyMedications.Add(medication);
            await _Context.SaveChangesAsync();

            // Now add the ingredients with the saved PharmacyMedicationId
            if (selectedIngredients != null && ingredientStrengths != null)
            {
                for (int i = 0; i < selectedIngredients.Count; i++)
                {
                    var ingredient = new PharmacyMedicationIngredient
                    {
                        PharmacyMedicationID = medication.PharmacyMedicationID,
                        ActiveID = selectedIngredients[i],
                        Strength = ingredientStrengths[i]
                    };
                    _Context.PharmacyMedicationIngredients.Add(ingredient);
                }
                await _Context.SaveChangesAsync();
            }

            ViewBag.getDosage = new SelectList(_Context.dosageForms, "DosageFormID", "DosageFormName");
            ViewBag.getActive = new SelectList(_Context.active, "ActiveID", "ActiveName");
            ViewBag.getSchedule = new SelectList(_Context.schedules, "ScheduleId", "ScheduleName");

            return RedirectToAction("IndexMedication");

        }
        public IActionResult updateMedication(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.pharmacyMedications.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            ViewBag.getDosage = new SelectList(_Context.dosageForms, "DosageFormID", "DosageFormName");
            ViewBag.getActive = new SelectList(_Context.active, "ActiveID", "ActiveName");
            ViewBag.getSchedule = new SelectList(_Context.schedules, "ScheduleId", "ScheduleName");
            return View(objList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateMedication(PharmacyMedication pharmacy)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.pharmacyMedications.Update(pharmacy);
            ViewBag.getDosage = new SelectList(_Context.dosageForms, "DosageFormID", "DosageFormName");
            ViewBag.getActive = new SelectList(_Context.active, "ActiveID", "ActiveName");
            ViewBag.getSchedule = new SelectList(_Context.schedules, "ScheduleId", "ScheduleName");
            _Context.SaveChanges();
            return RedirectToAction("IndexMedication");
        }
        public IActionResult updatePharmIndexOrder(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.order.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updatePharmIndexOrder(Order order)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.order.Update(order);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
            return RedirectToAction("PharmIndexOrder");
        }
        public IActionResult RejectPharmIndexOrder(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.order.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectPharmIndexOrder(Order order)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.order.Update(order);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
            return RedirectToAction("PharmIndexOrder");
        }
        [HttpPost]
        public IActionResult UpdatePharmPrescription(int userId)
        {
            var prescription = _Context.prescriptionViewModels.Find(userId);
            if (prescription == null)
            {
                return NotFound();
            }

            prescription.Status = "Dispensed";
            _Context.prescriptionViewModels.Update(prescription);
            _Context.SaveChanges(); // Ensure changes are saved

            return RedirectToAction("PharmPrescriptionList");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult updatePharmPrescription(Prescription prescription)
        //{
        //	//var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //	//bookSurgery.PatientID = user;
        //	_Context.prescriptions.Update(prescription);
        //	_Context.SaveChanges();
        //	return RedirectToAction("PharmPrescriptionList");
        //}
        public IActionResult RejectPharmPrescription(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var objList = _Context.prescriptions.Find(ID);
            if (objList == null)
            {
                return NotFound();
            }
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            return View(objList);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectPharmPrescription(Prescription prescription)
        {
            //var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bookSurgery.PatientID = user;
            _Context.prescriptions.Update(prescription);
            //ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");
            _Context.SaveChanges();
            return RedirectToAction("PharmPrescriptionList");
        }
        public IActionResult IndexStock()
        {
            IEnumerable<PharmStock> objList = _Context.pharmStock
              .Include(a => a.PharmacyMedication);
            return View(objList);

        }
        //Stock management
        // Action to display the order form
        public IActionResult OrderMedications()
        {
            var viewModel = new MedicationOrderView
            {
                Medications = _Context.pharmacyMedications
                    .Select(m => new PharmacyMedicationViewModel
                    {
                        PharmacyMedicationId = m.PharmacyMedicationID,
                        MedicationName = m.PharmacyMedicationName,
                        QuantityOnHand = m.stockhand,
                        ReorderLevel = m.stocklevel
                    }).ToList(),

                StockOrders = _Context.pharmStock
                    .Include(o => o.PharmacyMedication)
                    .Select(o => new StockOrderView
                    {
                        StockOrderId = o.PharmStockId,
                        MedicationName = o.PharmacyMedication.PharmacyMedicationName,
                        OrderQuantity = o.QuantityOrdered,
                        Date = o.Date,
                        Status = o.Status
                    }).ToList()
            };

            return View(viewModel);
        }

        // Action to process the order
        [HttpPost]
        public IActionResult OrderMedications(MedicationOrderView model)
        {
            foreach (var item in model.Medications.Where(m => m.IsSelected && m.OrderQuantity > 0))
            {
                var stockOrder = new PharmStock
                {
                    PharmacyMedicationID= item.PharmacyMedicationId,
                    Date = DateTime.Now,
                    QuantityOrdered = item.OrderQuantity,
                    Status = "Ordered"
                };

                _Context.pharmStock.Add(stockOrder);
            }

            _Context.SaveChanges();

            return RedirectToAction("IndexStock");
        }

        public IActionResult IndexRecieved()
        {
            IEnumerable<StockReceived> objList = _Context.StockReceiveds
              .Include(a => a.PharmStock)
              .ThenInclude(o => o.PharmacyMedication);
            return View(objList);

        }
        // GET: ReceivedMedication
        [HttpGet]
        public IActionResult ReceivedMedication()
        {
            var viewModel = new RecivedOrderViewModel
            {
                Medications = _Context.pharmStock
                    .Include(o => o.PharmacyMedication)
                    .Select(o => new SelectListItem
                    {
                        Value = o.PharmacyMedicationID.ToString(),
                        Text = o.PharmacyMedication.PharmacyMedicationName
                    })
                    .Distinct()
                    .ToList()
            };

            return View(viewModel);
        }

        // POST: ReceivedMedication
        // POST: ReceivedMedication
        [HttpPost]
        public async Task<IActionResult> ReceivedMedication(RecivedOrderViewModel model)
        {
            var medication = _Context.pharmacyMedications
                .SingleOrDefault(m => m.PharmacyMedicationID == model.SelectedMedicationId);

            if (medication != null)
            {
                // Find the related StockOrder
                var stockOrder = _Context.pharmStock
                    .FirstOrDefault(o => o.PharmacyMedicationID == model.SelectedMedicationId);

                if (stockOrder != null)
                {
                    // Create a new StockReceived for the received medication
                    var stockReceived = new StockReceived
                    {
                        PharmStockId = stockOrder.PharmStockId,
                        Date = DateTime.Now,
                        QuantityRecived = model.QuantityReceived
                    };

                    // Set the status of the related StockOrder
                    stockOrder.Status = "Received";
                    _Context.pharmStock.Update(stockOrder);

                    // Add the new StockReceived entry to the database
                    _Context.StockReceiveds.Add(stockReceived);

                    // Update the medication quantity
                    medication.stockhand += model.QuantityReceived;
                    _Context.pharmacyMedications.Update(medication);

                    await _Context.SaveChangesAsync();

                    // Redirect to a success page or back to the form
                    return RedirectToAction("IndexRecieved");
                }
                else
                {
                    ModelState.AddModelError("", "Stock order not found.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Selected medication is not found.");
            }

            // If model state is invalid or medication/stock order not found, redisplay the form
            model.Medications = _Context.pharmStock
                .Include(o => o.PharmacyMedication)
                .Select(o => new SelectListItem
                {
                    Value = o.PharmacyMedicationID.ToString(),
                    Text = o.PharmacyMedication.PharmacyMedicationName
                })
                .Distinct()
                .ToList();

            return View(model);
        }





    }
}
