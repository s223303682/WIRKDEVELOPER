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
            IEnumerable<Prescription> list = _Context.prescriptions;
            return View(list);
        }
        public IActionResult AllPrescriptionList()
        {
            IEnumerable<Prescription> list = _Context.prescriptions;
            return View(list);
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
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication);
            return View(objList);
            //IEnumerable<Order> objList = _Context.order;
            //return View(objList);

        }
        public IActionResult AllIndexOrder()
        {
            IEnumerable<Order> objList = _Context.order.Include(a => a.PharmacyMedication);
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
		public IActionResult updatePharmPrescription(int? ID)
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

			return View(objList);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult updatePharmPrescription(Prescription prescription)
		{
			//var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
			//bookSurgery.PatientID = user;
			_Context.prescriptions.Update(prescription);
			_Context.SaveChanges();
			return RedirectToAction("PharmPrescriptionList");
		}
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
