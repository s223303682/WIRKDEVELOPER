using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class AnVitals
	{
		[DisplayName("Vital Name")]
		public string? Vital { get; set; }
		[ForeignKey("VitalID")]

		[DisplayName("Reading")]
		public string? Reading { get; set; }
		[ForeignKey("VitalID")]

		[DisplayName("Time")]
		public string? Time { get; set; }
		[ForeignKey("VitalID")]

		[Key]
		public int AnVitalID { get; set; }
	}
}
