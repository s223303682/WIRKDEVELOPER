using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        //public int?Patient { get; set; }
        //[ForeignKey("PatientID")]
        //public virtual Patient? patient { get; set; }
        [Required]
        [DisplayName("Patient Name")]
        public string? Name { get; set; }
        [DisplayName("Patient Surname")]
        public string? Surname { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        [DisplayName("Email address")]
        public string? EmailAddress { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public DateTime? Time { get; set; }
        //public int?Theatre { get; set; }
        //[ForeignKey("TheatreID")]
        //public virtual Theatre? Theatre { get; set; }

        //[Required]
        //[DisplayName("Treatment Code")]

        //public string? TreatmentCodeID { get; set; }
        //[ForeignKey("TreatmentCodeID")]
        //public virtual TreatmentCode? TreatmentCode { get; set; }
        //[Required]

        //public string? AnasthesiologistID { get; set; }
        //[ForeignKey("AnasthesiologistID")]
        //public virtual Anasthesiologist? Anasthesiologist { get; set; } [Key]
        
    }
}
