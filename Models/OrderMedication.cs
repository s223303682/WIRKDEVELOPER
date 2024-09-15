using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public int Quantity { get; set; }
        public string Instructions { get; set; }

        // Navigation properties
        //public Order order { get; set; }
        //public PharmacyMedication PharmacyMedication { get; set; }
    }
}
