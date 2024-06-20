using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class VitalRanges
	{
		[Key]
		public int VitalRangeID { get; set; }

		[Required]
		[DisplayName(" Date")]
		public DateTime? Date { get; set; } = DateTime.Now;

		[Required]
		public string? Vital { get; set; }

		[Required]
		[DisplayName("Minimum Range")]
		public string? Minimumrange { get; set; }

		[Required]
		[DisplayName("Maximum Range ")]
		public string? Maximumrange { get; set; }

		[Required]
		public string? Units { get; set; }

	}
}
