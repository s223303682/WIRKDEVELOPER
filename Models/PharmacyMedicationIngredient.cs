using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class PharmacyMedicationIngredient
    {
        [Key]
        public int PharmacyMedicationIngredientId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [ForeignKey("PharmacyMedicationId")]
        public int PharmacyMedicationID { get; set; } // Corrected
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        [Required(ErrorMessage = "*Required")]
        [ForeignKey("ActiveIngredientId")]
        public int ActiveID { get; set; }
        public virtual Active Active { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Strength { get; set; }
    }
}
