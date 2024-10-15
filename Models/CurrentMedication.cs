using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
    public class CurrentMedication
    {
        [Key]
        public int CurrentMedicationID { get; set; } 
        public int  MedicationID { get; set; }
        [ForeignKey("MedicationID")]
        [DisplayName("Current Medication")]
        public virtual Medication?Medication { get; set; }
    }
}
