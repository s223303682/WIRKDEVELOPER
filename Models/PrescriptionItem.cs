using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class PrescriptionItem
    {

        [Key]
        public int PrescriptionItemID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string? Intructions { get; set; }
        //public string? MedicationID { get; set; }
        //[ForeignKey("MedicationID")]
        //public virtual ApplicationUser? User { get; set; }
        public int? PrescriptionID { get; set; }
        [ForeignKey("PrescriptionID")]
        public virtual Prescription? prescription { get; set; }
    }
}
