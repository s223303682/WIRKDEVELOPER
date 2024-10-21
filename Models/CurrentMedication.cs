using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
    public class CurrentMedication
    {
        [Key]
        public int CurrentMedicationID { get; set; }
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey(nameof(Medication))]
        public int MedicationId { get; set; }
        public virtual Medication? Medication { get; set; }


    }
}
