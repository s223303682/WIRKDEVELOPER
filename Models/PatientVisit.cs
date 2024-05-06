using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class PatientVisit
    {
        [Key] 
        public int PatientVisitID { get; set; }
        [Required]
        public DateAndTime? Date {  get; set; }
        [Required]
        public DateTime? AdmissionTime { get; set; }
        [Required]
        public DateTime? DischargeTime { get; set; }
        [Required]
        public int? Weight { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? patient { get; set; }
        public string? BedID { get; set; }
        [ForeignKey("BedID")]
        public virtual Bed? bed { get; set; }
        //public string?NurseID { get; set; }
        //[ForeignKey("NurseID")]
        //public virtual Nurse? nurse { get; set; }

    }
}
