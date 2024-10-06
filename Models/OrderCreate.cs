using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WIRKDEVELOPER.Models
{
    public class OrderCreate
    {
        [Key]
        public int OrderCreateID { get; set; }

        [Required]
        [DisplayName("Order")]
        public int? AnOrderID { get; set; }

        [ForeignKey("AnOrderID")]
        public virtual Order Order { get; set; }

        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Medication Ordered")]
        public int PharmacyMedicationID { get; set; }

        [ForeignKey("PharmacyMedicationID")]
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; } = "Ordered";  // Typo correction

        [Required]
        [DisplayName("Patient")]
        public int? AddmID { get; set; }

        [ForeignKey("AddmID")]
        public virtual Patient Patient { get; set; }  // Renamed for consistency

        [DisplayName("Urgent")]
        public string? Urgent { get; set; }

        public string? Notes { get; set; }

        // List of medications to be added dynamically
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}


