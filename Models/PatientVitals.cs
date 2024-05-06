using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace WIRKDEVELOPER.Models
{
    public class PatientVitals
    {
        [Key]
        public int PatientVitalsID { get; set; }
        [Required]
        public DateTime? Time { get; set; }
        [Required]
        public string? Reading {  get; set; }
        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }
    }
}
