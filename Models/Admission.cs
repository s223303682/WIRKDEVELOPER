using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        public int AdmissionID { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public int WardID { get;set; }
        [ForeignKey("WardID")]
        public int BedID { get; set; }
        [ForeignKey("BedID")]
        public int ConditionID { get; set; }
        [ForeignKey("ConditionID")]
        public DateTime AdmissionDate { get; set; }
       
       
    }
}
