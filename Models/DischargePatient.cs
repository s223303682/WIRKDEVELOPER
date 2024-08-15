using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class DischargePatient
    {
        public int DischargePatientId { get; set; }
        public string PatientName { get; set; }
        [ForeignKey("PatientId")]
       
        public string MedicationName { get; set; }
        [ForeignKey("MedicationID")]
        public int PatientGender { get; set; }
        [ForeignKey("PatientID")]
        public int staffId { get; set; }
        [ForeignKey("StaffId")]
        public DateTime AdmissionDate { get; set; }
        [ForeignKey("AdmissionId")]
        public DateTime DischargeDate { get; set; }
    }
}
