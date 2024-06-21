using System;
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
		public string? Patient { get; set; }
		[ForeignKey("AdmissionID")]



		[Required]
		[DisplayName("Medication Ordered")]
		public string? Medicationordered { get; set; }


		[Required]
		[DisplayName("Qunatity ")]
		public int? Quantity { get; set; }


		[Required]
		[DisplayName("Instructions")]
		public string? Instructions { get; set; }


		[DisplayName("Urgent")]
		public string? Urgent { get; set; }



		[DisplayName("Status")]
		public string? Status { get; set; }


		[Key]
		public int AnOrderID { get; set; }
	}
}
