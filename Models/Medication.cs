using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Medication
    {
        [Key]
        public int MedicationID { get; set; }
        [Required]
        [DisplayName("Medication Name")]
        public string? MedicationName { get; set; }
        [Required]
        [DisplayName("Active Ingridience")]
        public string? ActiveIngridience { get; set; }
        [Required]
        [DisplayName("DosageForm")]
        public string? DosageForm { get; set; }
        [Required]
        [DisplayName("Re-stock level")]
        public int RestockLevel { get; set; }
        [Required]
        public string Schedule { get; set; }
    }
}
