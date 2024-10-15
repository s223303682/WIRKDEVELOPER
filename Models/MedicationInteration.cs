using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WIRKDEVELOPER.Models
{
    public class MedicationInteration
    {
        [Key]
        public int MedicationInterationID { get; set; }

        [Required]
        [DisplayName("Medication Name")]
        public string? MedicationName { get; set; }
    }
}
