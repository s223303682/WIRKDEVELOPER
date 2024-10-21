using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class MedicationPrescription
    {
        [Key]
        public int MedicationPrescriptionID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [ForeignKey("PharmacyMedicationId")]
        public int PharmacyMedicationID { get; set; } // Corrected
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        [ForeignKey("Prescription")]
        public int Prescription { get; set; }
        public virtual Prescription? Prescriptions { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Quantity { get; set; }
        public int QuantityGiven { get; set; } = 0;
        [Required(ErrorMessage = "*Required")]
        public string Instruction { get; set; }

    }
}
