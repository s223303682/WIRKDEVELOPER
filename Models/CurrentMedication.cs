using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
    public class CurrentMedication
    {
        [Key]
        public int CurrentMedicationID { get; set; } 
        public string CurrentMedicationName { get; set; }
    }
}
