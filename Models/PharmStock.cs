﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class PharmStock
    {
        [Key]
        public int PharmStockId { get; set; }
        [Required]
		[DisplayName("Quantity")]
		public int QuantityOrdered { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; } = "Oredred";

		[Required]
		[DisplayName("Quantity")]
		public int QuantityRecieved { get; set; }
		[Required]
		[DisplayName(" Date")]
		public DateTime? Date { get; set; } = DateTime.Now;
        [DisplayName("Medication")]
        [Required(ErrorMessage = "Required")]
        [ForeignKey("Medication")]
        public int PharmacyMedicationID { get; set; }
        public virtual PharmacyMedication PharmacyMedication { get; set; }


    }
}
