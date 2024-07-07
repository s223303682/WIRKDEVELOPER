using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class AnCurrentMedication
	{
		[DisplayName("Medication Name")]
		public string? MedicationName { get; set; }
		[ForeignKey("AdmissionID")]

		[DisplayName("Dosage")]
		public string? Dosage { get; set; }
		[ForeignKey("AdmissionID")]


		[Key]
		public int CurrentMedicationID { get; set; }
	}
}
