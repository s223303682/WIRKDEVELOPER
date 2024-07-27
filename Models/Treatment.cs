using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Treatment
    {
        public int TreatmentID { get;set;}
        public string? TreatmentName { get; set; }
        [Required]
        public int TreatmentCode { get;set; }
        
    }
}
