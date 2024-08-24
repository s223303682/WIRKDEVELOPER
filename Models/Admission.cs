using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        [Key]
        public int AdmissionID { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")] 
        public int PatientGender { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Gender")] 
        public int MedicationName { get; set; }
        [ForeignKey("CurrentMedicationID")]
        [DisplayName("Current Medication")] 
        public int TreatmentCode { get; set; }
        [ForeignKey("TreatmentID")]
        [DisplayName("Treatment Code")] 
        public int PatientPhone { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Phone")]
        public int PatientEmail { get; set; }
        [ForeignKey("PatientID")] 
        [DisplayName("Email")]
        public int PatientProvince { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Province")]
        public string Address1 { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Address 1")]
        public string Address2 { get; set; }
        [ForeignKey("PatientID")] 
        [DisplayName("Address 2")]
        public string City { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("City")]
        public string Surbub { get; set; }
        [ForeignKey("PatientID")]
        public int BedID { get; set; }
        [ForeignKey("BedID")]
        public int ConditionID { get; set; }
        [ForeignKey("ConditionID")]
        public int WardID { get; set; }
        [ForeignKey("WardID")] 
        public int AllergiesID { get; set; }
        [ForeignKey("AlergiesID")] 
        public int MedicationID { get; set; }
        [ForeignKey("MedicationID")] 
        public int TreatmentID { get; set; }
        [ForeignKey("TreatmentID")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;
    }
}
