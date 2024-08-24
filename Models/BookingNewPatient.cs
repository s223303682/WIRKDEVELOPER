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
		public string BookingNewPatientIDNUmber { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Suburb { get; set; }
		public string Zip { get; set; }
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
        public int UserId { get; set; }
        [ForeignKey("AnaestesiologistID")]
        public virtual Anaesthesiologist? Anaestesiologist { get; set; }
    }
   
}
