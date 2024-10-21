using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;


namespace WIRKDEVELOPER.Models.Admin
{
    public class ChronicMedication
    {
        [Key]
        public int ChronicMedicationID { get; set; }

        public string ChronicName { get; set; }

        //[Required]
        //[DisplayName("ActiveIngridients")]
        //public int? ActiveID { get; set; }
        //[ForeignKey("ActiveID")]
        //public virtual Active Active { get; set; }

        [Required]
        [DisplayName("Schedule")]
        public int? ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public virtual Schedule Schedule { get; set; }

        [Required]
        [DisplayName("Dosage Form")]
        public int? DosageFormID { get; set; }
        [ForeignKey("DosageFormID")]
        public virtual DosageForm DosageForm { get; set; }

        [Required]
        [DisplayName("Active Ingredient")]
        public int? ActiveID { get; set; }
        [ForeignKey("ActiveID")]
        public virtual Active Active { get; set; }
    }
}
