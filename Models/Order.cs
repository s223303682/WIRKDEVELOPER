﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

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
        public virtual Patient Addm { get; set; }



        [Required]
		[DisplayName("Medication Ordered")]
		public int PharmacyMedicationID { get; set; }
        [ForeignKey("PharmacyMedicationID")]
        public virtual PharmacyMedication PharmacyMedication { get; set; }


		[Required]
		[DisplayName("Qunatity ")]
		public int? Quantity { get; set; }


		[Required]
		[DisplayName("Instructions")]
		public string? Instructions { get; set; }


		[DisplayName("Urgent")]
		public string? Urgent { get; set; }



		[DisplayName("Status")]
		public string? Status { get; set; } = "Odered";

        [DisplayName("Notes")]
        public string? Notes { get; set; }

        //[DisplayName("Status")]
        //public string? rejectorder { get; set; }

        [Key]
		public int AnOrderID { get; set; }

        // Navigation properties
        public ICollection<OrderMedication> orderMedications { get; set; }
        /*public ICollection<OrderItems> OrderItems { get; set; }*/ // Collection of order items
        //public virtual ICollection<AddOrder> AddOrders { get; set; }

    }
}
