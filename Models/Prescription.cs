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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Prescriber { get; set; }
        [Required]
        public string Urgent { get; set; }
        [Required]
        public string Status { get; set; }
        public string? IgnoreReason { get; set; } = "none";
        // Navigation propertiesnine
        public virtual ICollection<PrescriptionMedication> PrescriptionMedications { get; set; } = new List<PrescriptionMedication>();
     
    }

    public class PrescriptionViewModel
    {
        [Key]
        public int PrescriptionViewModelID { get; set; } // Consider renaming this to avoid confusion with PrescriptionID
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Prescriber { get; set; }
        [Required]
        public string Urgent { get; set; }
        [Required]
        public string Status { get; set; }
        public string IgnoreReason { get; set; }

        // List of medications to be added dynamically
        public List<PrescriptionMedicationViewModel> Medications { get; set; } = new List<PrescriptionMedicationViewModel>();
    }

    public class PrescriptionMedication
    {
        [Key]
        public int PrescriptionMedicationID { get; set; }
        public int PrescriptionID { get; set; }
        public int PharmacyMedicationID { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public string Instructions { get; set; }

        // Navigation properties
        public virtual Prescription Prescription { get; set; }
        public virtual PharmacyMedication PharmacyMedication { get; set; }
    }

    public class PrescriptionMedicationViewModel
    {
        [Key]
        public int PrescriptionMedicationViewModelID { get; set; }
        [Required]
        public string PharmacyMedicationName { get; set; } // Change from ID to Name
        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }

}

