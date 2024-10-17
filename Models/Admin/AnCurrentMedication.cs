using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models.Admin
{
    public class AnCurrentMedication
    {
        [Key]
        public int AnCurrentMedicationID { get; set; }

        [DisplayName("Medication")]
        public int ChronicMedicationID { get; set; }
        [ForeignKey("ChronicMedicationID")]
        public virtual ChronicMedication ChronicMedication { get; set; }




    }
}
