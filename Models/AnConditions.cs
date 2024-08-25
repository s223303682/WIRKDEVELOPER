using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class AnConditions
	{
        //[Required]
        //[DisplayName("Patient")]
        //public int? AddmID { get; set; }
        //[ForeignKey("AddmID")]
        //public virtual Patient Addm { get; set; }
        [DisplayName("Condition Name")]
		public string? Condition { get; set; }
		//[ForeignKey("AdmissionID")]

		[Key]
		public int AnConditionID { get; set; }
	}
}
