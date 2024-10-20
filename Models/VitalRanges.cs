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
		[DisplayName("Date")]
		public DateTime? Date { get; set; } = DateTime.Now;
		//[Required]
		//[DisplayName("Visit Time")]
		//public DateTime? Time { get; set; } = DateTime.Now;

		[Required]
		[DisplayName("Vital")]
		public string? Vital { get; set; }
		//[Required]
		//[DisplayName("Patient Name")]
		//public string? PatientName { get; set; }

		[Required]
		[DisplayName("Minimum Range")]
		public string? Minimumrange { get; set; }

		[Required]
		[DisplayName("Maximum Range ")]
		public string? Maximumrange { get; set; }
		//[Required]
		//[DisplayName("Weight")]
		//public string? Weight { get; set; }
		//[Required]
		//[DisplayName("Height")]
		//public string? Height { get; set; }

		[Required]
		public string? Units { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
