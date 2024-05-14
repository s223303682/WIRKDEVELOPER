using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        public int AdmissionID { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public DateTime AdmissionDate { get; set; }
        public int AllergyID { get; set; }
        [ForeignKey("AllergyID")]
        public int ConditionID { get; set; }
        [ForeignKey("ConditionID")]
        public int MedicatioID { get; set; }
        [ForeignKey("MedicationID")]
        public int BedID { get; set; }
        [ForeignKey("BedID")]
        public int WardID { get; set; }
        [ForeignKey("WardID")]
        public int ProcedureID { get; set; }
        [ForeignKey("ProcedureID")]
    }
}
