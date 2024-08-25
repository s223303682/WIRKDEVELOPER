using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class AddOrder
    {
        [Key]
        public int AddOrderID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [ForeignKey("PharmacyMedicationId")]
        public int PharmacyMedicationID { get; set; } // Corrected
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Instructions { get; set; }
    }
}
