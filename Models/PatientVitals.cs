using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace WIRKDEVELOPER.Models
{
    public class PatientVitals
    {
        [Key]
        public int PatientVitalsID { get; set; }
        [Required]

        public DateTime? Time { get; set; }
        [Required]
        
        public double? Reading2 { get; set; }
        [ForeignKey("PatientID")]
        public double Reading { get; set; }
        [ForeignKey("Booking")]
        public int BookingNewPatientID { get; set; }
        public virtual BookingNewPatient? BookingNewPatient { get; set; }
        public int? PatientID { get; set; }
        [ForeignKey("PatientID")]
        //public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; }
        [ForeignKey("Vitals")]
        public int VitalID { get; set; }
        public virtual Vitals? Vitals { get; set; }
        [NotMapped]
        public string? Note { get; set; }
        [NotMapped]
        public int? BookingId { get; set; }
    }
}
