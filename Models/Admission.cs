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
        //public string PatientSurname { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Patient Surname")]
        //public string PatientGender { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Gender")] 
        //public string MedicationName { get; set; }
        //[ForeignKey("CurrentMedicationID")]
        //[DisplayName("Current Medication")] 
        //public int TreatmentCode { get; set; }
        //[ForeignKey("TreatmentID")]
        //[DisplayName("Treatment Code")] 
        //public int PatientPhone { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Phone")]
        //public string PatientEmail { get; set; }
        //[ForeignKey("PatientID")] 
        //[DisplayName("Email")]
        //public string PatientProvince { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Province")]
        //public string Address1 { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Address 1")]
        //public string Address2 { get; set; }
        //[ForeignKey("PatientID")] 
        //[DisplayName("Address 2")]
        //public string City { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("City")]
        //public string Surbub { get; set; }
        //[ForeignKey("PatientID")]
        public int BedID { get; set; }
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
        //public string PostalCode { get; set; }
        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;
    }
}
