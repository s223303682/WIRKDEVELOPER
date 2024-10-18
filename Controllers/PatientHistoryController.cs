using Microsoft.AspNetCore.Mvc;
using WIRKDEVELOPER.Models;
using WIRKDEVELOPER.Models.PatientHistory;
using System.Linq;
using WIRKDEVELOPER.Areas.Identity.Data;

namespace WIRKDEVELOPER.Controllers
{
    public class PatientHistoryController : Controller
    {
        private readonly ApplicationDBContext _context; // Replace with your actual DbContext

        public PatientHistoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: PatientHistory
        public IActionResult Index(string name, string surname, string idNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(idNumber))
            {
                return NotFound();
            }

            // Find the patient by ID number
            var patient = _context.patients.FirstOrDefault(p => p.PatientIDNO == idNumber);
            if (patient == null)
            {
                return NotFound();
            }

            // Retrieve patient history
            var patientHistoryViewModel = new PatientHistoryViewModel
            {
                PatientID = patient.PatientID,
                Patient = patient,
                Allergies = _context.PatientAllergies
                    .Where(pa => pa.PatientID == patient.PatientID)
                    .Select(pa => pa.Active)
                    .ToList(),
                AnConditions = _context.PatientChronicConditions
                    .Where(pcc => pcc.PatientID == patient.PatientID)
                    .Select(pcc => pcc.AnConditions)
                    .ToList(),
                ChronicMedication = _context.PatientMedications
                    .Where(pm => pm.PatientID == patient.PatientID)
                    .Select(pm => pm.ChronicMedication)
                    .ToList(),
            };

            return View(patientHistoryViewModel);
        }
    }
}
