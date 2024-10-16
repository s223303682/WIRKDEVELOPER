﻿using Microsoft.AspNetCore.Mvc;
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
            var patient = _Context.bookingNewPatients.Where(e => e.Date == searchDate.Date).ToList();
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

            
            
                var order = new Order
                {
                    Date = viewModel.Date,
                    AddmID = viewModel.AddmID,
                    Urgent = viewModel.Urgent,
                    Status = viewModel.Status,

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
        public async Task<IActionResult> EditOrder(int id, OrderCreate viewModel)
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
                return RedirectToAction("IndexOrders");
            

            // Repopulate the dropdowns if validation fails
            ViewBag.getPatient = new SelectList(_Context.patients, "PatientID", "PatientName", viewModel.AddmID);
            ViewBag.getMedication = new SelectList(_Context.pharmacyMedications, "PharmacyMedicationID", "PharmacyMedicationName");

            return View(viewModel); // Return the view with validation errors
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            // Retrieve the order by ID, including related patient data
            var order = await _Context.order
                .Include(o => o.Addm)
                .Include(o => o.OrderMedications)          // Include the related medications for display
            .ThenInclude(om => om.PharmacyMedication)
                .FirstOrDefaultAsync(o => o.AnOrderID == id);

            if (order == null)
            {
                return NotFound(); // Return 404 if the order is not found
            }

            return View(order); // Display the order details on the delete confirmation page
        }
        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve the order along with its related medications
            var order = await _Context.order
                .Include(o => o.OrderMedications)
                .FirstOrDefaultAsync(o => o.AnOrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            // Remove related medications first
            _Context.ordermedication.RemoveRange(order.OrderMedications);

            // Remove the order
            _Context.order.Remove(order);

            // Save changes to the database
            await _Context.SaveChangesAsync();

            return RedirectToAction("IndexOrders"); // Redirect to the order list after deletion
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
            // Retrieve the list of orders from the database, including related entities if needed
            var orders = _Context.order
                .Include(o => o.Addm)                   // Include related Patient entity
                /*.Include(o => o.OrderItems) */                 // Include related OrderItems
                .Include(o => o.OrderMedications)                  // Include related OrderItems
                .ThenInclude(o => o.PharmacyMedication)
                  .Where(o => o.Status == "Received")                                                                //.ToListAsync()

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
       
        public IActionResult CreateNoteForOrder(int ID)
        {
            // Retrieve the order by ID, including related patient data
            var order =  _Context.order
                //.Include(o => o.Addm)
                .Include(o => o.OrderMedications)          
            .ThenInclude(om => om.PharmacyMedication)
                .FirstOrDefault(o => o.AnOrderID == ID);

            if (order == null)
            {
                return NotFound();
            }
            // Prepare model with medications for the order
            var model = new OrderCreate
            {
                AnOrderID = order.AnOrderID,
                OrderItems = order.OrderMedications.Select(m => new OrderItems
                {
                    PharmacyMedicationID = m.PharmacyMedicationID,
                    PharmacyMedication = m.PharmacyMedication,
                    Quantity = m.Quantity,
                    Instructions = m.Instructions,
                    Notes = m.Notes // Include existing notes if any
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult CreateNoteForOrder(OrderCreate viewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var medication in viewModel.OrderItems)
                {
                    // Find the OrderMedication entry by order ID and medication ID
                    var orderMedication = _Context.ordermedication
                        .FirstOrDefault(m => m.AnOrderID == viewModel.AnOrderID && m.PharmacyMedicationID == medication.PharmacyMedicationID);

                    if (orderMedication != null)
                    {
                        // Update the notes for the medication
                        orderMedication.Notes = medication.Notes;
                        orderMedication.Date = DateTime.Now;
                    }
                }

                _Context.SaveChanges();
                return RedirectToAction("ListNot");
            }

            return View(viewModel); // Return with validation errors if any
        }
        // ListNotes method to display all notes for received medications
        public IActionResult ListNot()
        {
            var notes = _Context.ordermedication
                .Include(om => om.PharmacyMedication)
                .Include(om => om.Order.Addm) // Include patient details
                .Where(om => om.Notes != null)
                .Select(om => new MedicationNotesViewModel
                {
                    PatientName = om.Order.Addm.PatientName,
                    MedicationName = om.PharmacyMedication.PharmacyMedicationName,
                    Notes = om.Notes,
                    Date = om.Date, // Display the date when notes were created
                    AnOrderID = om.AnOrderID
                }).ToList();

            return View(notes);
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

        // GET: Notes/List
        public async Task<IActionResult> ListNotes()
        {
            // Fetch orders with medications that have been marked as "received" and include notes
            var receivedMedications = await _Context.ordermedication
                .Include(om => om.PharmacyMedication)
                .Include(o => o.Order)
                .ThenInclude(om => om.Addm)
                .Include(om => om.notes) // Include notes here
                .Where(om => om.Order.Status == "Received")
                .ToListAsync();

            return View(receivedMedications);
        }

        // GET: Notes/Create
        public IActionResult CreateNote(int orderMedicationID)
        {
            var medication = _Context.ordermedication
                .Include(om => om.PharmacyMedication)
                 .Include(om => om.Order)
                .ThenInclude(o => o.Addm)
                .FirstOrDefault(om => om.OrderMedicationID == orderMedicationID);

            if (medication == null)
            {
                return NotFound();
            }

            // Populate the view model with relevant information
            var viewModel = new NotesOfOrders
            {
                OrderMedicationID = medication.OrderMedicationID,
                PharmacyMedicationName = medication.PharmacyMedication.PharmacyMedicationName,
                PatientName = medication.Order.Addm.PatientName,
                NoteText = "" // Initialize as empty
            };

            return View(viewModel);
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNote(NotesOfOrders viewModel)
        {
            //if (ModelState.IsValid)
            
                var note = new Notes
                {
                    OrderMedicationID = viewModel.OrderMedicationID,
                    NoteText = viewModel.NoteText,
                    CreatedAt = DateTime.Now
                };

                _Context.notes.Add(note);
                await _Context.SaveChangesAsync();

                return RedirectToAction("ListNotes");
            

            // If the model is invalid, return the same view
            return View(viewModel);
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











