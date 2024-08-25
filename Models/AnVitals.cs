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
        //[Required]
        //[DisplayName("Patient")]
        //public int? AddmID { get; set; }
        //[ForeignKey("AddmID")]
        //public virtual Patient Addm { get; set; }

        [DisplayName("Vital Name")]
		public string? Vital { get; set; }
		//[ForeignKey("VitalID")]

		[DisplayName("Reading")]
		public string? Reading { get; set; }
		//[ForeignKey("VitalID")]

		[DisplayName("Time")]
		public string? Time { get; set; }
		//[ForeignKey("VitalID")]

		[Key]
		public int AnVitalID { get; set; }
	}
}
