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
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
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
        public virtual ICollection<BookingPatientTreatmentCode> TreatmentCodes { get; set; } = new List<BookingPatientTreatmentCode>();
        public int? UserId { get; set; } // Change this to string
        [ForeignKey("UserId")]
        public virtual Anaesthesiologist? Anaesthesiologist { get; set; }
        public int? UserID { get; set; } // Change this to string
        [ForeignKey("UserId")]
        public virtual Surgeon? Surgeon { get; set; }
        [DisplayName("Ward")]
        public string? Ward { get; set; } = " - ";
        [DisplayName("Bed")]
        public string? Bed { get; set; } = " - ";
        [DisplayName("Status")]
        public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; }
        public string? Status { get; set; } = "Not Admitted ";
    }
    public class BookingPatientTreatmentCode
    {
        [Key]
        public int BookingPatientTreatmentCodeID { get; set; }

        public int BookingNewPatientID { get; set; }
        [ForeignKey("BookingNewPatientID")]
        public virtual BookingNewPatient BookingNewPatient { get; set; }

        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentCodeID")]
        public virtual TreatmentCode TreatmentCode { get; set; }
    }
    public class BookingListViewModel
    {
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
        public string OperationTheatreName { get; set; }
        public string AnaesthesiologistName { get; set; }
        public List<string> TreatmentCodes { get; set; }
    }

}
