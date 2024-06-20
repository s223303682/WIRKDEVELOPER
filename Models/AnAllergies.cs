using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class AnAllergies
	{
		[DisplayName("Active-Ingridient")]
		public string? ActiveIngridient { get; set; }
		[ForeignKey("AdmissionID")]

		[DisplayName("Reaction")]
		public string? Reaction { get; set; }
		[ForeignKey("AdmissionID")]

		[DisplayName("Status")]
		public string? Status { get; set; }
		[ForeignKey("AdmissionID")]

		[Key]
		public int AllergiesID { get; set; }
	}
}
