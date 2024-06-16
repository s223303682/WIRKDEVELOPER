using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class MedicationAdministration
    {
        public int MedicationAdministrationID { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public DateTime IssuedDate { get; set; }
        public int MedicationID { get; set; }
        [ForeignKey("MedicationID")]
       
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
        public DateTime Time { get; set; }
    }
}
