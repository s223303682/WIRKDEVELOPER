using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        [Key]
        public int AdmissionID { get; set; }
        [DisplayName("Patient Name")]
        public int PatientName { get; set; }
        [ForeignKey("PatientID")] 
        [DisplayName("Gender")]
        public int PatientGender { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Email")]
        public int PatientEmail { get; set; }
        [ForeignKey("PatientID")] 
        [DisplayName("Province")]
        public int PatientProvince { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Address 2")]
        public string Address2 { get; set; }
        [ForeignKey("PatientID")] 
        [DisplayName("City")]
        public string City { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Surbub")]
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
    }
}
