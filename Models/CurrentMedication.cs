using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WIRKDEVELOPER.Models
{
    public class CurrentMedication
    {
        [Key]
        public int CurrentMedicationID { get; set; } 
        public string CurrentMedicationName { get; set; }
    }
}
