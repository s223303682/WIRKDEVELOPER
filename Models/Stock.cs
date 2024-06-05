using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Stock
    {
        [Key]
        public int stockID { get; set; }
        [Required]
        public int? MedicationID { get; set; }
        [ForeignKey("MedicationName")]
        public virtual Medication? Medication { get; set; }
        [Required]
        public string? Restocklevel { get; set; }
        [ForeignKey("restovkLevel")]
        public virtual Medication? restocklevel { get; set; }
        [Required]
        [DisplayName("stock on hand")]
        public int? stockOnhand { get; set; }
        [Required]
        [DisplayName("stock  receive")]
        public int? stockreceive { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string? status { get; set; }
    }
}
