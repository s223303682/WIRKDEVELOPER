using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace WIRKDEVELOPER.Models
{
    public class Vitals
    {
        [Key]
        public int VitalID { get; set; }
        [Required]
        [DisplayName("Vital Name")]
        public string? VitalName { get; set; }
        [Required]
        [DisplayName("Low Limit")]
        public int? LowLimit { get; set; }
        [Required]
        [DisplayName("High Limit")]
        public int? HighLimit { get; set; }

    }
}
