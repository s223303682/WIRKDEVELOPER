using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class PharmacyMedication
    {
        [Key]
        public int PharmacyMedicationID { get; set; }
        [Required]
        public string? PharmacyMedicationName { get; set; }
        [Required]
        public int stocklevel { get; set; }
        [Required]
        public int stockhand { get; set; }
        [Required]
        public string Strength { get; set; }
    }
}
