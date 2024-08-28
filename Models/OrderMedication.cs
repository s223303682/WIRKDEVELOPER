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
        public int AnOrderID { get; set; }
        public int PharmacyMedicationID { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }

        // Navigation properties
        public Order order { get; set; }
        public PharmacyMedication PharmacyMedication { get; set; }
    }
}
