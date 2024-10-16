﻿using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WIRKDEVELOPER.Models
{
    public class OrderItems
    {
        [Key]
        public int OrderItemsID { get; set; }

        [Required]
        [DisplayName("Medication Ordered")]
        public int PharmacyMedicationID { get; set; }

        [ForeignKey("PharmacyMedicationID")]
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        [Required]
        public int Quantity { get; set; }  // Consider adding validation for positive quantity

        [Required]
        public string? Instructions { get; set; }

        [DisplayName("Notes")]
        public string? Notes { get; set; }
    }
}

