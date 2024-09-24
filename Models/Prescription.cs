using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Prescriber { get; set; }
        public string Urgent { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public ICollection<PrescriptionMedication> PrescriptionMedications { get; set; }
    }
    public class PrescriptionViewModel
    {
        [Key]
        public int PrescriptionViewModelID { get; set; }
        public string Name { get; set; }
        public string surname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Prescriber { get; set; }
        public string Urgent { get; set; }
        public string Status { get; set; }


        // List of medications to be added dynamically
        public List<PrescriptionMedicationViewModel> Medications { get; set; } = new List<PrescriptionMedicationViewModel>();
    }

    public class PrescriptionMedication
    {
        [Key]
        public int PrescriptionMedicationID { get; set; }
        public int PrescriptionID { get; set; }
        public int PharmacyMedicationID { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }

        // Navigation properties
        public Prescription Prescription { get; set; }
        public PharmacyMedication PharmacyMedication { get; set; }
    }


    public class PrescriptionMedicationViewModel
    {
        [Key]
        public int PrescriptionMedicationViewModelID { get; set; }
        public int PharmacyMedicationID { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }
}
