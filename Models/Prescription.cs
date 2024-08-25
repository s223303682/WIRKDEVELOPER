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
        public int PrescriptionID { get; set; } // Unique identifier for the prescription

        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Prescriber { get; set; }
        public string Urgent { get; set; }
        public string Status
        {
            get; set;

        }
        [Required]
        public int PharmacyMedicationID { get; set; }
        [ForeignKey("PharmacyMedicationID")]
        public virtual PharmacyMedication? PharmacyMedication { get; set; }

        public int Quantity { get; set; }
        public string Instructions { get; set; }

        public List<PharmacyMedication> Medications { get; set; } = new List<PharmacyMedication>();
    }
   
}
