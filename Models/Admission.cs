using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        [Key]
        public int AdmissionID { get; set; }
        public string PatientName { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Patient Name")]
        public virtual Patient Patient { get; set; }
        public string PatientGender { get; set; }
        [DisplayName("Gender")]

        public int PatientPhone { get; set; }
        [DisplayName("Phone")]

        public string PatientEmail { get; set; }
        [DisplayName("Email")]

        public string province { get; set; }
        [DisplayName("Province")]

        public string Address1 { get; set; }
        [DisplayName("Address 1")]

        public string Address2 { get; set; }
        [DisplayName("Address 2")]

        public string City { get; set; }
        [DisplayName("City")]

        public string Surbub { get; set; }

        public int BedNumber { get; set; }
        [ForeignKey("BedID")]
        public string ConditionName { get; set; }
        [ForeignKey("ConditionID")]
        public string WardName { get; set; }
        [ForeignKey("WardID")] 
        public string AllergiesName { get; set; }
        [ForeignKey("AlergiesID")] 
        public string MedicationName { get; set; }
        [ForeignKey("MedicationID")] 
        public int TreatmentCode { get; set; }
        [ForeignKey("TreatmentID")]
        public string zip { get; set; }
        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;
    }
}
