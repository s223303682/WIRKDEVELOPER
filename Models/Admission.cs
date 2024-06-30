using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        public int AdmissionID { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public string Address1 { get; set; } 
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; } 
        public int BedID { get; set; }
        [ForeignKey("BedID")]
        public int ConditionID { get; set; }
        [ForeignKey("ConditionID")]
        public int WardID { get; set; }
        [ForeignKey("WardID")]
        public string PostalCode { get; set; }
    }
}
