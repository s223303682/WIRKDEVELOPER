using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Treatment
    {
        public int TreatmentID { get;set;}
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public int TreatmentCode { get;set; }
        
    }
}
