using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Account;
namespace WIRKDEVELOPER.Models
{
    public class BookingNewPatient
    {

        [Key]
        public int BookingNewPatientID { get; set; }
		public string BookingNewPatientName { get; set; }
		public string BookingNewPatientSurname { get; set; }
        [Required(ErrorMessage = "ID number is required.")]
        [StringLength(13, ErrorMessage = "ID number cannot exceed 15 characters.")]
        [RegularExpression(@"^[A-Za-z0-9]{13}$", ErrorMessage = "ID number must be alphanumeric and exactly 10 characters long.")]
        public string BookingNewPatientIDNUmber { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Province { get; set; }
		public string City { get; set; }
		public string Suburb { get; set; }
		public string Zip { get; set; }
        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(10, ErrorMessage = "Contact number cannot exceed 15 characters.")]
        [RegularExpression(@"^\+?[1-9]\d{1,10}$", ErrorMessage = "Contact number must be a valid phone number.")]
        public string ContactNumber { get; set; }
		[Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }
        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }
        [Required(ErrorMessage = "Required")]
        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentCodeID")]
        public virtual TreatmentCode? treatmentCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? AnaesthesiologistID { get; set; }

        [ForeignKey("AnaesthesiologistID")]
        public virtual Anaesthesiologist? Anaesthesiologist { get; set; }
        [DisplayName("Ward")]
		public string? Ward { get; set; } = " - ";
		[DisplayName("Bed")]
		public string? Bed { get; set; } = " - ";
		[DisplayName("Status")]
		public string? Status { get; set; } = "Not Admitted ";
	}
   
}
