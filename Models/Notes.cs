using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class Notes
	{
		[Required]
		[DisplayName(" Date")]
		public DateTime? Date { get; set; } = DateTime.Now;

		[Required]
		[DisplayName("Patient")]
		public int? AnOrderID { get; set; }
		[ForeignKey("AnOrderID")]

		[Required]
		[DisplayName("Medication Ordered")]
		public string? Medicationordered { get; set; }
		[ForeignKey("AnOrderID")]

		[Required]
		[DisplayName("Qunatity ")]
		public int? Quantity { get; set; }
		[ForeignKey("AnOrderID")]

		[Required]
		[DisplayName("Notes")]
		public string? notes { get; set; }

		[Key]
		public int NotesID { get; set; }
	}
}
