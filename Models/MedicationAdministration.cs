using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class MedicationAdministration
    {
        public int MedicationAdministrationID { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public DateTime IssuedDate { get; set; }
        public DateTime TimeIssued { get; set; } 
        public int Quantity { get; set; } 
        public string Status { get; set; }
        public int MedicationID { get; set; }
        [ForeignKey("MedicationID")]
       
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public DateTime Time { get; set; }
    }
}
