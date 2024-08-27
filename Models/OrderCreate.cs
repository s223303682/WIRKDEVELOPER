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
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Patient")]
        public int? AddmID { get; set; }
        [ForeignKey("AddmID")]
        public virtual Patient Addm { get; set; }

        public string? Notes { get; set; }

        public List<PharmacyMedication> Medications { get; set; }

        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>
        {
            new OrderItems() // Add one empty item initially
        };
    }
}
