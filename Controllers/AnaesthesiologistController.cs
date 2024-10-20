﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
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
using System.Diagnostics;
using System.Web.WebPages;
using WIRKDEVELOPER.Models.Admin;
using WIRKDEVELOPER.Models.sendemail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WIRKDEVELOPER.Controllers
{
    public class AnaesthesiologistController : Controller
    {
        private readonly ApplicationDBContext _Context;
        private readonly EmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnaesthesiologistController(ApplicationDBContext applicationDBContext, EmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _Context = applicationDBContext;
            _emailService = emailService;
            _userManager = userManager;
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
        public IActionResult IndexOrders( DateTime date)
        {
            // Retrieve the list of orders from the database, including related entities if needed
            var orders = _Context.order
                .Include(o => o.Addm)                   // Include related Patient entity
                /*.Include(o => o.OrderItems) */                 // Include related OrderItems
                .Include(o => o.OrderMedications)                  // Include related OrderItems
                .ThenInclude(o => o.PharmacyMedication)
                        .Where(o => o.Date >= date)// Include related PharmacyMedication entity
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int AnOrderID) // Make sure the parameter name matches your hidden input
        {
            // Retrieve the order along with its related medications
            var order = await _Context.order
                .Include(o => o.OrderMedications)
                .FirstOrDefaultAsync(o => o.AnOrderID == AnOrderID);

            if (order == null)
            {
                return NotFound(); // Return 404 if the order is not found
            }

            // Remove related medications first
            _Context.ordermedication.RemoveRange(order.OrderMedications);

            // Remove the order
            _Context.order.Remove(order);

            // Save changes to the database
            await _Context.SaveChangesAsync();

            return RedirectToAction("IndexOrders"); // Redirect to the order list after deletion
        }

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
        public async Task<IActionResult> VitalRanges(VitalRanges vitalranges)
        {
            if (ModelState.IsValid)
            {
                _Context.vitalranges.Update(vitalranges);
                _Context.SaveChanges();

                // Prepare email details
                string subject = "New Vital Range Added";
                string body = $"A new vital range has been added:\n" +
                              $"Vital: {vitalranges.Vital}\n" +
                              $"Minimum Range: {vitalranges.Minimumrange}\n" +
                              $"Maximum Range: {vitalranges.Maximumrange}\n" +
                              $"Units: {vitalranges.Units}\n" +
                              $"Date: {vitalranges.Date?.ToString("g")}";

                // Use the email provided in the VitalRanges form
                string recipientEmail = vitalranges.Email;

                // Send the email notification
                await _emailService.SendEmailAsync(recipientEmail, subject, body);

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

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateVitalRanges(int ID, VitalRanges vitalRanges)
        {


            _Context.vitalranges.Update(vitalRanges);
            _Context.SaveChanges();
            return RedirectToAction("IndexVitalRanges");

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

            
        }


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

        // GET: Create Note
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

        // POST: Create Note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNote(NotesOfOrders viewModel)
        {
            var note = new Notes
            {
                OrderMedicationID = viewModel.OrderMedicationID,
                NoteText = viewModel.NoteText,
                CreatedAt = DateTime.Now
            };

            _Context.notes.Add(note);
            await _Context.SaveChangesAsync();

            return RedirectToAction("ListNotes");
        }

        // GET: Update Note
        public IActionResult UpdateNote(int orderMedicationID)
        {
            var note = _Context.notes
                .FirstOrDefault(n => n.OrderMedicationID == orderMedicationID);

            if (note == null)
            {
                return NotFound();
            }

            var viewModel = new NotesOfOrders
            {
                OrderMedicationID = note.OrderMedicationID,
                NoteText = note.NoteText
            };

            return View(viewModel);
        }

        // POST: Update Note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNote(NotesOfOrders viewModel)
        {
            var note = _Context.notes
                .FirstOrDefault(n => n.OrderMedicationID == viewModel.OrderMedicationID);

            if (note == null)
            {
                return NotFound();
            }

            note.NoteText = viewModel.NoteText;
            note.CreatedAt = DateTime.Now;

            _Context.notes.Update(note);
            await _Context.SaveChangesAsync();

            return RedirectToAction("ListNotes");
        }

        // POST: Delete Note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNote(int orderMedicationID)
        {
            var note = _Context.notes
                .FirstOrDefault(n => n.OrderMedicationID == orderMedicationID);

            if (note == null)
            {
                return NotFound();
            }

            _Context.notes.Remove(note);
            await _Context.SaveChangesAsync();

            return RedirectToAction("ListNotes");
        }

        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var orders = await _Context.order
                .Include(o => o.OrderMedications)
                .ThenInclude(om => om.PharmacyMedication)
                .Include(o => o.Addm)
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();

            var reportData = new List<AnestheticReportViewModel>();
            var medicineSummary = new Dictionary<string, int>();

            foreach (var order in orders)
            {
                foreach (var medication in order.OrderMedications)
                {
                    reportData.Add(new AnestheticReportViewModel
                    {
                        Date = order.Date?.ToString("d"),
                        PatientName = order.Addm?.PatientName,
                        MedicationName = medication.PharmacyMedication?.PharmacyMedicationName,
                        Quantity = medication.Quantity
                    });

                    var medicationName = medication.PharmacyMedication?.PharmacyMedicationName;
                    if (medicationName != null)
                    {
                        if (medicineSummary.ContainsKey(medicationName))
                        {
                            medicineSummary[medicationName] += medication.Quantity;
                        }
                        else
                        {
                            medicineSummary[medicationName] = medication.Quantity;
                        }
                    }
                }
            }

            var medicineSummaryList = medicineSummary.Select(ms => new MedicineSummaryViewModel
            {
                MedicationName = ms.Key,
                QuantityOrdered = ms.Value
            }).ToList();

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.MedicineSummary = medicineSummaryList;

            return View(reportData);
        }
        public class PageNumberHelper : iTextSharp.text.pdf.PdfPageEventHelper
        {
            // This will write the page number at the bottom of each page
            public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
            {
                // Create a content byte writer
                var cb = writer.DirectContent;
                // Create a footer text for the page number
                var footerText = $"Page {writer.PageNumber}";
                var font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL);
                var footerPhrase = new iTextSharp.text.Phrase(footerText, font);

                // Set up the positioning of the footer (centered at the bottom)
                var pageSize = document.PageSize;
                var x = (pageSize.Left + pageSize.Right) / 2;
                var y = pageSize.GetBottom(30); // 30 units from the bottom

                // Show the page number
                iTextSharp.text.pdf.ColumnText.ShowTextAligned(cb, iTextSharp.text.Element.ALIGN_CENTER, footerPhrase, x, y, 0);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DownloadReport(DateTime startDate, DateTime endDate)
        {
            var orders = await _Context.order
                .Include(o => o.OrderMedications)
                .ThenInclude(om => om.PharmacyMedication)
                .Include(o => o.Addm)
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();

            var user = await _userManager.GetUserAsync(User);
            var anesthesiologistName = $"{user.FirstName} {user.LastName}";

            var medicineSummary = new Dictionary<string, int>();

            using (var stream = new MemoryStream())
            {
                // Initialize iTextSharp 5 Document and PdfWriter
                var document = new iTextSharp.text.Document();
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, stream);

                // Add the page number event helper
                var pageNumberHelper = new PageNumberHelper();
                writer.PageEvent = pageNumberHelper;

                document.Open();

                // Add report header
                document.Add(new iTextSharp.text.Paragraph("Anesthetic Report"));
                document.Add(new iTextSharp.text.Paragraph($"Date Range: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd}"));
                document.Add(new iTextSharp.text.Paragraph($"Report Generated: {DateTime.Now:yyyy-MM-dd}"));
                document.Add(new iTextSharp.text.Paragraph($"Prepared by: Dr. {anesthesiologistName}"));

                document.Add(new iTextSharp.text.Paragraph("\n"));

                // Create a table for report data
                var table = new iTextSharp.text.pdf.PdfPTable(4);
                table.AddCell("Date");
                table.AddCell("Patient");
                table.AddCell("Medication");
                table.AddCell("Quantity");

                // Add report data to the table
                foreach (var order in orders)
                {
                    foreach (var medication in order.OrderMedications)
                    {
                        table.AddCell(order.Date?.ToString("d"));
                        table.AddCell(order.Addm?.PatientName);
                        table.AddCell(medication.PharmacyMedication?.PharmacyMedicationName);
                        table.AddCell(medication.Quantity.ToString());

                        var medicationName = medication.PharmacyMedication?.PharmacyMedicationName;
                        if (medicationName != null)
                        {
                            if (medicineSummary.ContainsKey(medicationName))
                            {
                                medicineSummary[medicationName] += medication.Quantity;
                            }
                            else
                            {
                                medicineSummary[medicationName] = medication.Quantity;
                            }
                        }
                    }
                }

                // Add the table to the document
                document.Add(table);

                // Add total patients count
                var totalPatients = orders.Select(o => o.Addm?.PatientName).Distinct().Count();
                document.Add(new iTextSharp.text.Paragraph($"\nTotal Patients: {totalPatients}\n"));

                // Add medicine summary section
                document.Add(new iTextSharp.text.Paragraph("Medicine Summary:\n"));
                var summaryTable = new iTextSharp.text.pdf.PdfPTable(2);
                summaryTable.AddCell("Medicine");
                summaryTable.AddCell("Quantity Ordered");

                foreach (var summary in medicineSummary)
                {
                    summaryTable.AddCell(summary.Key);
                    summaryTable.AddCell(summary.Value.ToString());
                }

                // Add the summary table to the document
                document.Add(summaryTable);

                // Close the document
                document.Close();

                // Return the PDF file as a download
                return File(stream.ToArray(), "application/pdf", "AnestheticReport.pdf");
            }
        }






        public async Task<IActionResult> ViewPatientHistory(int id) // id is the AddmID
        {
            var admission = await _Context.addm
        .Include(a => a.Patient)
        .ThenInclude(p => p.PatientAllergies)
        .ThenInclude(pa => pa.Active)
        .Include(a => a.Patient)
        .Include(pc => pc.Patient.PatientChronicCondition)
        .ThenInclude(pc => pc.AnConditions)
        .Include(a => a.Patient.PatientMedication)
        .ThenInclude(pm => pm.ChronicMedication)
        .FirstOrDefaultAsync(a => a.PatientID == id);

            if (admission == null)
            {
                return NotFound($"No admission found for AddmID: {id}");
            }

            var patient = admission.Patient;
            if (patient == null)
            {
                return NotFound($"No patient associated with the AddmID: {id}");
            }

            return View(new List<Addm> { admission });
        }



    }

}             
