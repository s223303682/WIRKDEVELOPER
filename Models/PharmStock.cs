using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class PharmStock
    {
        [Key]
        public int PharmStockId { get; set; }
        [Required]
		[DisplayName("Quantity")]
		public int QuantityOrdered { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; } = "Oredred";

		[DisplayName(" Date")]
		public DateTime? Date { get; set; } = DateTime.Now;

        [DisplayName("Medication")]
        [Required(ErrorMessage = "Required")]
        [ForeignKey("PharmacyMedicationID")]
        public int PharmacyMedicationID { get; set; }
        public virtual PharmacyMedication PharmacyMedication { get; set; }

        public List<PharmacyMedication> MedicationEntries { get; set; } = new List<PharmacyMedication>();


    }
}
