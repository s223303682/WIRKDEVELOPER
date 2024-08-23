using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class PharmacyMedication
    {
        [Key]
        public int PharmacyMedicationID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PharmacyMedicationName { get; set; }
        [Required(ErrorMessage = "Required")]
        public int stocklevel { get; set; }
        [Required(ErrorMessage = "Required")]
        public int stockhand { get; set; }
        

        [Required (ErrorMessage ="Required")]
        [ForeignKey("DosageForm")]
        public int DosageFormID { get; set; }
        public virtual DosageForm DosageForm { get; set; }

        [Required(ErrorMessage = "Required")]
        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }

        //[Required(ErrorMessage = "Required")]
        //[ForeignKey("Active")]
        //public int ActiveID { get; set; }
        //public virtual Active Active { get; set; }
        public virtual ICollection<PharmacyMedicationIngredient> Ingredients { get; set; }

      





    }
}
