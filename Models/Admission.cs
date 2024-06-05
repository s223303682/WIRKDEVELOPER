using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        public int AdmissionID { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get;set; }
        public DateTime AdmissionDate { get; set; }
        //public int AllergyID { get; set; }
        //[ForeignKey("AllergyID")]
        //public virtual Allergy? allergy { get; set; }
        //public int ConditionID { get; set; }
        //[ForeignKey("ConditionID")]
        //public virtual Condition? condition { get; set; }
        //public int MedicatioID { get; set; }
        //[ForeignKey("MedicationID")]
        //public virtual Medication? medication { get; set; }
        //public int BedID { get; set; }
        //[ForeignKey("BedID")]
        //public virtual Bed? bed { get; set; }
        //public int WardID { get; set; }
        //[ForeignKey("WardID")]
        //public virtual Ward? ward { get; set; }
       
    }
}
