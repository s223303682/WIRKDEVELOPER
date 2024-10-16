using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class OrderMedication
    {
        [Key]
        public int OrderMedicationID { get; set; }

        [DisplayName("Order")]
        public int? AnOrderID { get; set; }

        [ForeignKey("AnOrderID")]
        public virtual Order Order { get; set; }

        [DisplayName("Medication Ordered")]
        public int PharmacyMedicationID { get; set; }

        [ForeignKey("PharmacyMedicationID")]
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        [Required]
        public int Quantity { get; set; }  // Consider adding validation for positive quantity

        [Required]
        public string Instructions { get; set; }

        [DisplayName("Notes")]
        public string? Notes { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public ICollection<Notes> notes { get; set; }
    }
}


