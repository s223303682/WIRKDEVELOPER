using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class TreatmentCode
    {
        [Key]
        public int TreatmentCodeID { get; set; }
        [Required]
        [DisplayName("ICD-10 CODE")]
        public string? ICDCODE{ get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
