using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {


		[Key]
		public int PrescriptionID { get; set; }
        public string BookingID { get; set; }
        public string Name { get; set; }
        [Required]
		[DisplayName("Prescription Date")]
		public DateTime? Date { get; set; }
		public string? prescriber { get; set; }
		[ForeignKey("ID")]
		public virtual ApplicationUser? ApplicationUser { get; set; }
		[Required(ErrorMessage = "Required")]
		public int PharmacyMedicationID { get; set; }
		[ForeignKey("PharmacyMedicationID")]
		public virtual PharmacyMedication? PharmacyMedication { get; set; }
		[Required]
		[DisplayName("Quantity")]
		public int Quantity { get; set; }
		[Required]
		[DisplayName("Instructions")]
		public string? Instructions { get; set; }
		[Required]
		public string? Urgent { get; set; }
		[Required]
		public string? status { get; set; }
		[Required]
		public string? Description { get; set; }



	}
}
