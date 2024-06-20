using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class ViewPatient
	{
		[Key]
		public int ViewPatientID { get; set; }

		[DisplayName("Ward")]
		public string? Ward { get; set; }
		[ForeignKey("AdmissionID")]

		[DisplayName("Bed")]
		public string? Bed { get; set; }
		[ForeignKey("AdmissionID")]

		[DisplayName("Admission Status")]
		public string? Status { get; set; }
		[ForeignKey("AdmissionID")]

		[DisplayName("Patient")]
		public string? Patient { get; set; }
		[ForeignKey("BookingSurgeryID")]

		[Required]
		[DisplayName("Date")]
		public DateTime? Date { get; set; } = DateTime.Now;
	}
}
