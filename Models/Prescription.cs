using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {

        [Key]
        public int PrescriptionID { get; set; }
        public int? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }
        [Required]
        [DisplayName("Prescription Date")]
        public DateTime? Date { get; set; }
        //public string ? MedicationID { get; set; }
        //[ForeignKey("MedicationName")]
        //public virtual Medication? medication { get; set; }
        [Required]
        [DisplayName("Quantity And Instractions")]
        public string? QuantityAndInstractions { get; set; }
        [Required]
        public string? Urgent { get; set; }
        [Required]
        public string? status { get; set; }
        

       
        
    }
}
