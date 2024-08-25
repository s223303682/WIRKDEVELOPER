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
        //[Required]
        //[DisplayName("Patient")]
        //public int? AddmID { get; set; }
        //[ForeignKey("AddmID")]
        //public virtual Patient Addm { get; set; }

        [DisplayName("CurrentMedication")]
		public int MedicationID { get; set; }
		[ForeignKey("MedicationID")]
        public virtual Medication Medication{get; set; }

        //[DisplayName("Dosage")]
        //public string? Dosage { get; set; }
        //[ForeignKey("AdmissionID")]


        [Key]
		public int CurrentMedicationID { get; set; }
	}
}
