//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MimeKit;
//using MimeKit;
//using MailKit.Net.Smtp;
//using System.IO;
//using WIRKDEVELOPER.Areas.Identity.Data;
//using WIRKDEVELOPER.Areas.Identity.Data.VM;
//using WIRKDEVELOPER.Models;
//using Org.BouncyCastle.Utilities;

//namespace WIRKDEVELOPER.Controllers
//{
//    public class NurseController : Controller
//    {
//        private readonly ApplicationDBContext Context;
//        public NurseController(ApplicationDBContext DbContext)
//        {
//            Context = DbContext;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult Dashboard()
//        {
//            return View();
//        }
//        public async Task<IActionResult> ReceiveMeds(int i)
//        {
//            var x = await Context.prescriptions.Where(c => c.PrescriptionID == i).FirstOrDefaultAsync();
//            if (x == null)
//            {
//                return NotFound();
//            }
//            x.status = "Received";
//            x.NurseID = HttpContext.Session.GetInt32("userint");
//            Context.prescriptions.Update(x);
//            await Context.SaveChangesAsync();
//            return RedirectToActionPermanent(nameof(PrescribedPrescriptions));
//        }
//        public async Task<IActionResult> PatientHistory(int i)
//        {
//            var x = await Context.patients.Where(c => c.PatientID == i).FirstOrDefaultAsync();
//            if (x == null)
//            {
//                return NotFound();
//            }
//            var sub = new SubReport()
//            {
//                Patient = x,
//                PatientVitals = Context.patientVitals.Include(c => c.Vitals).Where(c => c.PatientID == i).OrderByDescending(c => c.Time).AsEnumerable().DistinctBy(c => c.VitalID),
//                Allergies = Context.anallergies.Include(c => c.Active).Where(c => c.PatientId == i).AsEnumerable().DistinctBy(c => c.ActiveId),
//                CurrentMedications = Context.CurrentMedications.Include(c => c.Medication).Where(c => c.PatientId == i).AsEnumerable().DistinctBy(c => c.MedicationId),
//                PatientConditions = Context.patientConditions.Include(c => c.Condition).Where(c => c.PatientID == i).AsEnumerable().DistinctBy(c => c.Condition),
//            };
//            return View(sub);
//        }

//        public async Task<IActionResult> RetakeVitals(int i)
//        {
//            var booking = await Context.bookings.Include(c => c.Patient).Where(x => x.BookingID == i).FirstOrDefaultAsync();
//            if (booking == null)
//            {
//                TempData["e"] = "Booking not found!";
//                return RedirectToAction(nameof(Bookings));
//            }
//            ViewBag.Vitals = new SelectList(Context.Vitals, "VitalID", "VitalType");
//            ViewBag.Vit = Context.Vitals.ToList();
//            var ad = new Admission()
//            {
//                Booking = booking,
//                BookingId = booking.BookingID,
//            };

//            return View(ad);
//        }

//        public async Task<IActionResult> Discharge(int i)
//        {
//            var x = await Context.admission.Include(c => c.Booking).Include(c => c.Booking.Patient).Where(c => c.AdmissionID == i).FirstOrDefaultAsync();
//            if (x == null)
//            {
//                return NotFound();
//            }
//            var a = new Discharge()
//            {
//                Admission = x
//            };
//            return View(a);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Discharge(Discharge a)
//        {
//            if (ModelState.IsValid)
//            {
//                await Context.Discharges.AddAsync(a);
//                await Context.SaveChangesAsync();
//                var ad = await Context.admission.Where(c => c.AdmissionID == a.AdmissionId).FirstOrDefaultAsync();
//                ad.Status = "Discharged";
//                Context.admission.Update(ad);
//                await Context.SaveChangesAsync();
//                var b = await Context.bookings.Where(c => c.BookingID == ad.BookingId).FirstOrDefaultAsync();
//                b.Status = "Discharged";
//                Context.bookings.Update(b);
//                await Context.SaveChangesAsync();
//                return RedirectToAction(nameof(AdmittedPatients));
//            }
//            return RedirectToAction(nameof(Discharge), new { i = a.AdmissionId });
//        }
//        [HttpPost]
//        public async Task<IActionResult> Report(DateTime from, DateTime to)
//        {
//            var meds = Context.PrescriptionMedicines.Include(c => c.Prescription).Include(c => c.Prescription.Patient).Include(c => c.PharmacyMedication).Where(c => c.Prescription.NurseID == HttpContext.Session.GetInt32("userint") && c.Prescription.Date >= from.Date && c.Prescription.Date <= to.Date).AsEnumerable();
//            var pat = Context.patients.AsEnumerable();
//            var v = (from p in pat
//                     join m in meds on p.PatientID equals m.Prescription.PatientID
//                     select new Report
//                     {
//                         PatientName = $"{p.PatientName} {p.PatientSurname}",
//                         PatientID = p.PatientID,
//                         Date = m.Prescription.Date.ToShortDateString(),
//                         Time = m.Prescription.Date.ToShortTimeString(),
//                         QTY = m.QuantityGiven,
//                         MedicationId = m.PharmacyMedicationID,
//                         Medication = m.PharmacyMedication.MedicationName
//                     }).AsEnumerable();
//            var s = v.GroupBy(x => x.Medication).Select(c => new { n = c.Key, i = c.Sum(c => c.QTY) }).AsEnumerable();

//            var x = new ReportList()
//            {
//                Reports = v,
//                Nurse = HttpContext.Session.GetString("names"),
//                from = from.ToShortDateString(),
//                to = to.ToShortDateString(),
//            };
//            return View(x);
//        }

//        public IActionResult PrescribedPrescriptions()
//        {
//            var pres = Context.prescriptions.Where(c => c.status == "Dispensed").AsEnumerable();
//            var ad = Context.admission.Include(c => c.Booking).Include(c => c.Bed).Include(c => c.Bed.Ward).Include(c => c.Booking.Patient).Where(c => c.NurseId == HttpContext.Session.GetInt32("userint") && c.Status == "Admitted").AsEnumerable();
//            var m = (from p in pres
//                     join a in ad on p.PatientID equals a.Booking.PatientID
//                     select new PresAd
//                     {
//                         PatientName = $"{a.Booking.Patient.PatientName} {a.Booking.Patient.PatientSurname} ",
//                         PatientID = a.Booking.Patient.PatientIDNO,
//                         Bed = $"{a.Bed.BedNumber}",
//                         Ward = a.Bed.Ward.WardName,
//                         PresId = p.PrescriptionID
//                     }).AsEnumerable();
//            return View(m);
//        }

//        public IActionResult Bookings()
//        {
//            return View(Context.bookings.Include(c => c.Patient).Include(c => c.OperationTheatre).Include(c => c.Anaestesiologist.ApplicationUser).Include(c => c.Surgeon.ApplicationUser).Where(c => c.Status == "Booked"));
//        }
//        public async Task<IActionResult> Admit(int i, int pat)
//        {
//            var booking = await Context.bookings.Include(c => c.Patient).Where(x => x.BookingID == i && x.PatientID == pat).FirstOrDefaultAsync();
//            if (booking == null)
//            {
//                TempData["e"] = "Booking not found!";
//                return RedirectToAction(nameof(Bookings));
//            }
//            ViewBag.Beds = new SelectList(Context.beds.Include(c => c.Ward).Where(c => c.IsAvailable).Select(c => new { i = c.BedID, n = $"{c.Ward.WardName} - {c.BedNumber}" }), "i", "n");
//            ViewBag.Active = new SelectList(Context.activeIngredients, "ActiveID", "ActiveName");
//            ViewBag.Condition = new SelectList(Context.conditionDiagnoses, "ConditionID", "ConditionName");
//            ViewBag.Meds = new SelectList(Context.medications, "MedicationID", "MedicationName");
//            ViewBag.Vitals = new SelectList(Context.Vitals, "VitalID", "VitalType");
//            ViewBag.Sub = new SelectList(Context.Suburbs.Include(c => c.City).OrderBy(c => c.Name).Select(c => new { c.Id, c.Name }), "Id", "Name");
//            ViewBag.Vit = Context.Vitals.ToList();
//            var ad = new Admission()
//            {
//                Booking = booking,
//                BookingId = booking.BookingID,
//                NurseId = (int)HttpContext.Session.GetInt32("userint")
//            };
//            return View(ad);
//        }

//        public IActionResult AdmittedPatients()
//        {
//            return View(Context.admission.Include(c => c.Booking.Patient).Include(c => c.Bed).Include(c => c.Bed.Ward).Include(c => c.Booking.OperationTheatre).Include(c => c.Booking.Surgeon).Include(c => c.Booking.Surgeon.ApplicationUser).Include(c => c.Booking.Anaestesiologist.ApplicationUser).Where(c => c.Status == "Admitted" && c.NurseId == HttpContext.Session.GetInt32("userint")));
//        }
//        public IActionResult PatientPrescriptions(int i)
//        {
//            return View(Context.prescriptions.Include(c => c.Surgeon).Include(c => c.Patient).Include(c => c.Surgeon.ApplicationUser).Where(c => c.PatientID == i && c.status == "Received").AsEnumerable());
//        }
//        public IActionResult AdministerMedicine(int i, int p)
//        {
//            return View(Context.PrescriptionMedicines.Include(c => c.PharmacyMedication).Include(c => c.Prescription).Include(c => c.Prescription.Patient).Include(c => c.Prescription.Surgeon).Include(c => c.Prescription.Surgeon.ApplicationUser).Where(c => c.PrescriptionID == i && c.Prescription.PatientID == p).AsEnumerable());
//        }


//        [HttpPost]
//        public async Task<IActionResult> AdministerMeds([FromBody] List<MedsID> meds)
//        {
//            if (meds.Any())
//            {
//                foreach (var item in meds)
//                {
//                    var x = await Context.PrescriptionMedicines.Where(c => c.PrescriptionMedicineID == item.MedId).FirstOrDefaultAsync();
//                    if (x != null)
//                    {
//                        x.QuantityGiven = x.QuantityGiven + item.Quantity;
//                        Context.PrescriptionMedicines.Update(x);
//                        await Context.SaveChangesAsync();
//                    }
//                }
//                return Json(true);
//            }
//            else
//            {
//                return Json(null);
//            }

//        }
//        [HttpPost]
//        public async Task<IActionResult> AdmitPatient(Admission admission)
//        {
//            if (ModelState.IsValid)
//            {
//                admission.NurseId = (int)HttpContext.Session.GetInt32("userint");
//                await Context.admission.AddAsync(admission);
//                await Context.SaveChangesAsync();
//                var x = Context.bookings.Where(c => c.BookingID == admission.BookingId).FirstOrDefault();
//                x.Status = "Admitted";
//                Context.bookings.Update(x);
//                await Context.SaveChangesAsync();
//                TempData["m"] = "Patient has been admitted!";
//                return RedirectToAction(nameof(AdmittedPatients));
//            }
//            TempData["m"] = "Something went wrong. Please try again";
//            return RedirectToAction(nameof(Admit), new { id = admission.BookingId, pat = admission.Pat });
//        }
//        [HttpPost]
//        public async Task<IActionResult> UpdatePatient([FromBody] PatientVM patient)
//        {
//            if (ModelState.IsValid)
//            {
//                var p = await Context.patients.Where(c => c.PatientID == patient.PatientID).FirstOrDefaultAsync();
//                if (p == null)
//                {
//                    return Json(null);
//                }
//                p.EmailAddress = patient.EmailAddress;
//                p.contactNumber = patient.contactNumber;
//                p.surbub = int.Parse(patient.surbub);
//                Context.patients.Update(p);
//                await Context.SaveChangesAsync();
//                return Json(true);
//            }
//            return Json(null);
//        }
//        [HttpPost]
//        public async Task<IActionResult> UpdatePatientVitals([FromBody] List<PatientVitals> vitals)
//        {
//            if (ModelState.IsValid)
//            {
//                await Context.patientVitals.AddRangeAsync(vitals);
//                await Context.SaveChangesAsync();
//                return Json(true);
//            }
//            return Json(null);
//        }
//        [HttpPost]
//        public async Task<IActionResult> UpdatePatientVitalsE([FromBody] List<PatientVitals> vitals)
//        {
//            if (ModelState.IsValid)
//            {
//                var pat = await Context.bookings.Include(c => c.Surgeon).Include(c => c.Surgeon.ApplicationUser).Where(c => c.BookingID == vitals[0].BookingId).FirstOrDefaultAsync();
//                var email = new MimeMessage();
//                email.From.Add(new MailboxAddress("Hospital", "noreply@hospital.com"));
//                email.To.Add(new MailboxAddress($"{pat.Surgeon.ApplicationUser.FirstName}", $"{pat.Surgeon.ApplicationUser.Email}"));
//                email.Subject = "Vitals Retaken";
//                var vital = Context.Vitals.AsEnumerable();
//                var m = (from v in vitals
//                         join v1 in vital on v.VitalID equals v1.VitalID
//                         select new
//                         {
//                             n = v1.VitalType,
//                             v = v.Reading,
//                             v1 = v.Reading2,
//                             t = v.Time.Value.ToString("G"),
//                         }).AsEnumerable();
//                var bodyBuilder = new BodyBuilder
//                {
//                    HtmlBody = $@"<table border='1'>
//                    <thead>
//                        <tr>
//                            <th>Vital</th>
//                            <th>Reading 1</th>
//                            <th>Reading 2</th>
//                            <th>Time</th>
//                        </tr>
//                    </thead>
//                    <tbody>
//                        {string.Join("", m.Select(v => $@"
//                            <tr>
//                                <td>{v.n}</td>
//                                <td>{v.v}</td>
//                                <td>{v.v1}</td>
//                                <td>{v.t}</td>
//                            </tr>"))}
//                    </tbody>
//                </table>
//                <h3>Notes from Nurse</h3>
//                <p>{vitals[0].Note}</p>"
//                };

//                email.Body = bodyBuilder.ToMessageBody();

//                using (var client = new SmtpClient())
//                {
//                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
//                    client.Authenticate("jfappels@gmail.com", "fkrq nfsj vvrp pxwk");
//                    await client.SendAsync(email);
//                    client.Disconnect(true);
//                }
//                await Context.patientVitals.AddRangeAsync(vitals);
//                await Context.SaveChangesAsync();
//                return Json(true);
//            }
//            return Json(null);
//        }

//        [HttpPost]
//        public async Task<IActionResult> UpdatePatientCon([FromBody] PatCon Con)
//        {
//            if (Con.PatConditions != null && Con.PatConditions.Any())
//            {
//                await Context.patientConditions.AddRangeAsync(Con.PatConditions);
//                await Context.SaveChangesAsync();

//            }
//            if (Con.Allergies != null && Con.Allergies.Any())
//            {
//                await Context.anallergies.AddRangeAsync(Con.Allergies);
//                await Context.SaveChangesAsync();

//            }
//            if (Con.Medications != null && Con.Medications.Any())
//            {
//                await Context.CurrentMedications.AddRangeAsync(Con.Medications);
//                await Context.SaveChangesAsync();

//            }
//            return Json(true);
//        }
//    }
//}
