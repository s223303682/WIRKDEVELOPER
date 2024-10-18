using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Admin;


namespace WIRKDEVELOPER.Models.PatientHistory
{
    public class PatientMedication
    {
        [Key]
        public int PatientMedicationId { get; set; }

        [ForeignKey("PatientId")]
        public int PatientID { get; set; }

        [ForeignKey("ChronicMedicationID")]
        public int? ChronicMedicationID { get; set; } = null;

        public virtual ChronicMedication ChronicMedication { get; set; }
    }
}
