﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Web.Helpers;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;
using WIRKDEVELOPER.Models.Admin;
using WIRKDEVELOPER.Models.PatientHistory;
using WIRKDEVELOPER.Models.sendemail;


namespace WIRKDEVELOPER.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly ApplicationDBContext _Context;
        private readonly EmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PharmacistController(ApplicationDBContext applicationDBContext, EmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _Context = applicationDBContext;
            _emailService = emailService;
            _userManager = userManager;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        // GET: Pharmacist/UpdatePrescription/5
        public IActionResult UpdatePrescription(int id)
        {
            var prescription = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                .ThenInclude(pm => pm.PharmacyMedication) // Include medication details if needed
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Pharmacist/UpdatePrescription/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePrescription(Prescription prescription)
        {
           
                var existingPrescription = _Context.prescriptions
                    .Include(pm => pm.PrescriptionMedications)
                    .ThenInclude(pm => pm.PharmacyMedication)
                    .FirstOrDefault(p => p.PrescriptionID == prescription.PrescriptionID);

                if (existingPrescription == null)
                {
                    return NotFound();
                }

                // Update fields
                existingPrescription.Name = prescription.Name;
                existingPrescription.Surname = prescription.Surname;
                existingPrescription.Gender = prescription.Gender;
                existingPrescription.Email = prescription.Email;
                existingPrescription.Date = prescription.Date;
                existingPrescription.Prescriber = prescription.Prescriber;
                existingPrescription.Urgent = prescription.Urgent;
                existingPrescription.Status = prescription.Status;

                // Adjust stock based on the status
                if (existingPrescription.Status == "Dispensed")
                {
                    foreach (var prescriptionMed in existingPrescription.PrescriptionMedications)
                    {
                        var pharmacyMed = _Context.pharmacyMedications.Find(prescriptionMed.PharmacyMedicationID);
                        if (pharmacyMed != null)
                        {
                            pharmacyMed.stockhand -= prescriptionMed.Quantity;
                        }
                    }
                }

                // Save changes to the database
                _Context.SaveChanges();

            return RedirectToAction("PharmPrescriptionList"); // Redirect after successful update
           

        }



        // GET: Pharmacist/RejectPrescription/5
        public IActionResult RejectPrescription(int id)
        {
            var prescription = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                .ThenInclude(pm => pm.PharmacyMedication) // Include medication details if needed
                .FirstOrDefault(p => p.PrescriptionID == id);

            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Pharmacist/RejectPrescription/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectPrescription(Prescription prescription)
        {
            // Find the existing prescription in the database
            var existingPrescription = _Context.prescriptions
                .FirstOrDefault(p => p.PrescriptionID == prescription.PrescriptionID);

            if (existingPrescription == null)
            {
                return NotFound();
            }

            // Update the existing prescription's status and ignore reason
            existingPrescription.Status = "Rejected"; // Explicitly set status to "Rejected"
            existingPrescription.IgnoreReason = prescription.IgnoreReason; // Store the rejection reason

            // Save the changes to the database
            _Context.Update(existingPrescription);
            _Context.SaveChanges();

            return RedirectToAction("PharmPrescriptionList"); // Redirect to the list view
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


        public IActionResult PharmPrescriptionList()
        {
            var prescriptions = _Context.prescriptions
                .Include(p => p.PrescriptionMedications)
                    .ThenInclude(pm => pm.PharmacyMedication) // Include related PharmacyMedication data
                .Select(p => new Prescription
                {
                    PrescriptionID = p.PrescriptionID,
                    Name = p.Name,
                    Surname = p.Surname, // Ensure this property is included
                    Gender = p.Gender,
                    Email = p.Email,
                    Date = p.Date,
                    Prescriber = p.Prescriber,
                    Urgent = p.Urgent,
                    Status = p.Status,
                    IgnoreReason = p.IgnoreReason,
                    PrescriptionMedications = p.PrescriptionMedications.Select(m => new PrescriptionMedication
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
                       PharmacyMedicationName = m.PharmacyMedication.PharmacyMedicationName,
                       Quantity = m.Quantity,
                       Instructions = m.Instructions
                   }).ToList()
               }).ToList();

            return View(prescriptions);
        }
     
        public IActionResult PharmIndexOrder()
        {
            var orders = _Context.order
                .Include(o => o.Addm)                   // Include related Patient entity
                /*.Include(o => o.OrderItems) */                 // Include related OrderItems
                .Include(o => o.OrderMedications)                  // Include related OrderItems
                .ThenInclude(o => o.PharmacyMedication)         // Include related PharmacyMedication entity
                                                                //.ToListAsync()

             .Select(item => new OrderCreate
             {
                 AnOrderID = item.AnOrderID,
                 Date = item.Date,
                 AddmID = item.AddmID,
                 Patient = item.Addm,
                 Urgent = item.Urgent,
                 Status = item.Status,
                 Notes = item.Notes,
                 OrderItems = item.OrderMedications.Select(m => new OrderItems
                 {
                     PharmacyMedication = m.PharmacyMedication,
                     Quantity = m.Quantity,
                     Instructions = m.Instructions,
                     Notes = m.Notes
                 }).ToList()
             }).ToList();

            //return View(prescriptions);

            return View(orders);
        }
        public IActionResult AllIndexOrder()
        {
            // Retrieve the list of orders from the database, including related entities if needed
            var orders = _Context.order
                .Include(o => o.Addm)                   // Include related Patient entity
                /*.Include(o => o.OrderItems) */                 // Include related OrderItems
                .Include(o => o.OrderMedications)                  // Include related OrderItems
                .ThenInclude(o => o.PharmacyMedication)         // Include related PharmacyMedication entity
                                                                //.ToListAsync()

             .Select(item => new OrderCreate
             {
                 Date = item.Date,
                 AddmID = item.AddmID,
                 Patient = item.Addm,
                 Urgent = item.Urgent,
                 Status = "Ordered",
                 OrderItems = item.OrderMedications.Select(m => new OrderItems
                 {
                     PharmacyMedicationID = m.PharmacyMedicationID,
                     Quantity = m.Quantity,
                     Instructions = m.Instructions
                 }).ToList()
             }).ToList();

            //return View(prescriptions);

            return View(orders);
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
        public async Task<IActionResult> OrderMedications()
        {
            var viewModel = new MedicationOrderView
            {
                Medications = _Context.pharmacyMedications
                    .Select(m => new PharmacyMedicationViewModel
                    {
                        PharmacyMedicationId = m.PharmacyMedicationID,
                        PharmacyMedicationName = m.PharmacyMedicationName,
                        QuantityOnHand = m.stockhand,
                        ReorderLevel = m.stocklevel
                    }).ToList(),

                StockOrders = _Context.pharmStock
                    .Include(o => o.PharmacyMedication)
                    .Select(o => new StockOrderView
                    {
                        StockOrderId = o.PharmStockId,
                        PharmacyMedicationName = o.PharmacyMedication.PharmacyMedicationName,
                        OrderQuantity = o.QuantityOrdered,
                        Date = o.Date,
                        Status = o.Status
                    }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> OrderMedications(MedicationOrderView model, string orderEmail)
        {
            // Check if the provided email is not empty
            if (string.IsNullOrWhiteSpace(orderEmail))
            {
                // Handle the case where email is not provided
                ModelState.AddModelError("OrderEmail", "Email is required.");
                return View(model); // Return to the view with error
            }

            // Add stock orders to the database
            foreach (var item in model.Medications.Where(m => m.IsSelected && m.OrderQuantity > 0))
            {
                var stockOrder = new PharmStock
                {
                    PharmacyMedicationID = item.PharmacyMedicationId,
                    Date = DateTime.Now,
                    QuantityOrdered = item.OrderQuantity,
                    Status = "Ordered"
                };

                _Context.pharmStock.Add(stockOrder);

                // Optionally, add to StockOrders if needed for email or other purposes
                model.StockOrders.Add(new StockOrderView
                {
                    PharmacyMedicationName = item.PharmacyMedicationName,
                    OrderQuantity = item.OrderQuantity,
                    Status = stockOrder.Status,
                    Date = stockOrder.Date,
                    Email = orderEmail // Use the single email address for all orders
                });
            }

            await _Context.SaveChangesAsync();

            // Send a single email for all stock orders
            if (model.StockOrders.Any())
            {
                var firstOrder = model.StockOrders.First(); // Take the first order to get email address
                string subject = "Order Confirmation";
                string body = $@"
                        <p>Dear Purchase Manager,</p>
                        <p> Order is confirmed with the following details:</p>
                        <table border='1' cellpadding='5' cellspacing='0'>
                            <thead>
                                <tr>
                                    <th>Medication Name</th>
                                    <th>Quantity Ordered</th>
                                    <th>Status</th>
                                    <th>Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {string.Join("", model.StockOrders.Select(o => $@"
                                    <tr>
                                        <td>{o.PharmacyMedicationName}</td>
                                        <td>{o.OrderQuantity}</td>
                                        <td>{o.Status}</td>
                                        <td>{o.Date?.ToString("MMMM dd, yyyy")}</td>
                                    </tr>
                                "))}
                            </tbody>
                        </table>
                        <p>Thank you !</p>
                        <p>Best regards,</p>
                        <p>Your Pharmacy</p>";

                try
                {
                    await _emailService.SendEmailAsync(orderEmail, subject, body);
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log it)
                    // Example: _logger.LogError(ex, "Failed to send email to {0}.", orderEmail);
                }
            }

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
            IEnumerable<Models.AnAllergies> objList = _Context.anallergies.Include(a => a.Active);
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
        public IActionResult Report()
        {
            return View();
        }

        public ActionResult DispensaryReport(DateTime? startDate, DateTime? endDate)
		{

            
			// Initialize a default view model
			var viewModel = new PharmacistReportViewModel
			{
				//PharmacistName = "Dororo",
				StartDate = startDate ?? DateTime.Now,  // Use current date as default
				EndDate = endDate ?? DateTime.Now,      // Use current date as default
				ReportGeneratedDate = DateTime.Now,
				PrescriptionItems = new List<PrescriptionReportItem>(),
				MedicationSummaries = new List<MedicationSummary>()
			};

			// Check if date range is invalid
			if (!startDate.HasValue || !endDate.HasValue)
			{
				ViewBag.ErrorMessage = "Please select a valid date range.";
				return View(viewModel);  // Pass the initialized view model
			}

			// Fetch prescription medications based on the date range
			var prescriptionMedications = _Context.prescriptionMedications
				.Include(pm => pm.Prescription)
				.Include(pm => pm.PharmacyMedication)
				.Where(pm => pm.Prescription.Date >= startDate.Value && pm.Prescription.Date <= endDate.Value)
				.ToList();

			// Populate the report items
			var reportItems = prescriptionMedications.Select(pm => new PrescriptionReportItem
			{
				Date = pm.Prescription.Date,
				PatientName = $"{pm.Prescription.Name} {pm.Prescription.Surname}",
				Prescriber = pm.Prescription.Prescriber,
				Medication = pm.PharmacyMedication.PharmacyMedicationName,
				Quantity = pm.Quantity,
				Status = pm.Prescription.Status
			}).ToList();

			// Calculate totals for the report
			var totalDispensed = reportItems.Count(ri => ri.Status == "Dispensed");
			var totalRejected = reportItems.Count(ri => ri.Status == "Rejected");

			// Create medication summary
			var medicationSummaries = prescriptionMedications
				.Where(pm => pm.Prescription.Status == "Dispensed")
				.GroupBy(pm => pm.PharmacyMedication.PharmacyMedicationName)
				.Select(group => new MedicationSummary
				{
					MedicationName = group.Key,
					TotalQuantityDispensed = group.Sum(pm => pm.Quantity)
				}).ToList();

			// Update the view model with the data
			viewModel.PrescriptionItems = reportItems;
			viewModel.TotalScriptsDispensed = totalDispensed;
			viewModel.TotalScriptsRejected = totalRejected;
			viewModel.MedicationSummaries = medicationSummaries;

			return View(viewModel);
		}
        public async Task<IActionResult> UpdateOrder(int id)
        {
            // Retrieve the order along with related medications and patient data
            var order = await _Context.order
                .Include(o => o.Addm)                      // Include the related patient
                .Include(o => o.OrderMedications)          // Include the related medications
                .ThenInclude(om => om.PharmacyMedication)  // Include the PharmacyMedication for each OrderMedication
                .FirstOrDefaultAsync(o => o.AnOrderID == id);

            if (order == null)
            {
                return NotFound(); // If the order does not exist
            }

            // Populate the ViewBag with dropdown data for medications and patients
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", order.AddmID);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");

            // Prepare the view model
            var model = new OrderCreate
            {
                AnOrderID = order.AnOrderID,
                Date = order.Date,
                AddmID = order.AddmID,
                Urgent = order.Urgent,
                Status = order.Status,
                OrderItems = order.OrderMedications.Select(m => new OrderItems
                {
                    PharmacyMedicationID = m.PharmacyMedicationID,
                    Quantity = m.Quantity,
                    Instructions = m.Instructions
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrder(int id, OrderCreate viewModel)
        {
            if (id != viewModel.AnOrderID)
            {
                return NotFound(); // Ensure the ID matches the viewModel
            }

            //if (ModelState.IsValid)

            try
            {
                // Retrieve the existing order
                var order = await _Context.order
                    .Include(o => o.OrderMedications)  // Include medications to update them
                    .FirstOrDefaultAsync(o => o.AnOrderID == id);

                if (order == null)
                {
                    return NotFound();
                }

                // Update the order details
                order.Date = viewModel.Date;
                order.AddmID = viewModel.AddmID;
                order.Urgent = viewModel.Urgent;
                order.Status = viewModel.Status;

                // Clear existing medications
                _Context.ordermedication.RemoveRange(order.OrderMedications);

                // Add updated medications
                foreach (var medication in viewModel.OrderItems)
                {
                    var ordermedication = new OrderMedication
                    {
                        AnOrderID = order.AnOrderID,
                        PharmacyMedicationID = medication.PharmacyMedicationID,
                        Quantity = medication.Quantity,
                        Instructions = medication.Instructions
                    };
                    _Context.ordermedication.Add(ordermedication);
                }

                // Save changes to the database
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_Context.order.Any(e => e.AnOrderID == id))
                {
                    return NotFound(); // Handle concurrency issues
                }
                else
                {
                    throw; // Re-throw if there’s another issue
                }
            }
            return RedirectToAction("PharmIndexOrder");


            // Repopulate the dropdowns if validation fails
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", viewModel.AddmID);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");

            return View(viewModel); // Return the view with validation errors
        }
        public async Task<IActionResult> ViewPatientHistory(int id)
        {
            var admissions = await _Context.addm
                .Include(a => a.Patient)
                .ThenInclude(p => p.PatientAllergies)
                .Include(a => a.Patient)
                .ThenInclude(p => p.PatientChronicCondition)
                .Include(a => a.Patient)
                .ThenInclude(p => p.PatientMedication)
                .Where(a => a.PatientID == id) // Assuming you have a PatientId in your Admissions
                .ToListAsync();

            if (admissions == null || !admissions.Any())
            {
                return NotFound(); // Handle the case where no admissions are found
            }

            return View(admissions);
        }


    }
}
