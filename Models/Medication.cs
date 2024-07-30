using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Medication
    {
        [Key]
        public int MedicationID { get; set; }
        
        [Required]
        [DisplayName("Medication Name")]
        public string? MedicationName { get; set; }
        [Required]
        public int DosageFormID{ get; set; }
        [ForeignKey("DosageFormName")]
        public virtual DosageForm? DosageFormN { get; set; }
        [Required]
        [DisplayName("Active Ingridient")]
        public int ActiveIngridientID { get; set; }
        [ForeignKey("ActiveIngridientID")]
        public virtual ActiveIngredient? ActiveIngredient { get; set; }
        [Required]
        [DisplayName("Active Ingredient strength")]
        public string? ActiveIngredientStrength { get; set; }
		[Required]
		public string? Schedule { get; set; }
		[Required]
		public int QuantityOnHand { get; set; }
		[Required]
        [DisplayName("Re-stock level")]
        public int RestockLevel { get; set; }
       
    }
}
