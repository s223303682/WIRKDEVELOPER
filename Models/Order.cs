using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WIRKDEVELOPER.Models
{
    public class Order
    {

        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Patient")]
        public int? AddmID { get; set; }

        [ForeignKey("AddmID")]
        public virtual Patient Addm { get; set; }  // Consider renaming to "Patient" for consistency

        [DisplayName("Urgent")]
        public string? Urgent { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; } = "Ordered";  // Typo correction

        [DisplayName("Notes")]
        public string? Notes { get; set; }

        [Key]
        public int AnOrderID { get; set; }

        // Navigation property for the medications linked to the order
        public ICollection<OrderMedication> OrderMedications { get; set; }  // Adjust to plural for clarity
    }
}


