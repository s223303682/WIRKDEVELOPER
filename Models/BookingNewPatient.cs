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
        [Required(ErrorMessage = "ID Number is required.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ID Number must be 13 digits long.")]
        public string BookingNewPatientIDNUmber { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be 10 digits.")]
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string Zip { get; set; }
       
        [Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }
        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }
        [Required(ErrorMessage = "Required")]
        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentCodeID")]
        public virtual TreatmentCode? treatmentCode { get; set; }
        public int? UserId { get; set; } // Change this to string
        [ForeignKey("UserId")]
        public virtual Anaesthesiologist? Anaesthesiologist { get; set; }
        [DisplayName("Ward")]
        public string? Ward { get; set; } = " - ";
        [DisplayName("Bed")]
        public string? Bed { get; set; } = " - ";
        [DisplayName("Status")]
        public string? Status { get; set; } = "Not Admitted ";
    }

}
