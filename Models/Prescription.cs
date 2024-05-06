using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {

        [Key]
        public int PrescriptionID { get; set; }
        [Required]
        [DisplayName("Prescription Date")]
        public DateTime? Date { get; set; }
        [Required]
        public string? Urgent { get; set; }
        [Required]
        public string? status { get; set; }
        //public string? SurgeonID { get; set; }
        //[ForeignKey("SurgeonID")]
        //public virtual Prescription? prescription { get; set; }

        public int? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }
        //public string? PharmacistID { get; set; }
        //[ForeignKey("PharmacistID")]
        //public virtual Pharmacist  pharmacist{ get; set; }
    }
}
