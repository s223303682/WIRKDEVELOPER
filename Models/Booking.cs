using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Account;
namespace WIRKDEVELOPER.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }

        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }

        
        public virtual ICollection<BookingTreatmentCode> BookingTreatmentCodes { get; set; } = new List<BookingTreatmentCode>();

        public int? UserId { get; set; } // Change this to string
        [ForeignKey("UserId")]
        public virtual Anaesthesiologist? Anaesthesiologist { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; } = "Admitted ";
    }


    // Join table to represent the many-to-many relationship
    public class BookingTreatmentCode
{
    [Key]
    public int BookingTreatmentCodeID { get; set; }

    public int BookingID { get; set; }
    [ForeignKey("BookingID")]
    public virtual Booking Booking { get; set; }

    public int TreatmentCodeID { get; set; }
    [ForeignKey("TreatmentCodeID")]
    public virtual TreatmentCode TreatmentCode { get; set; }
}
    public class BookingViewModel
    {
        [Key]
        public int BookingViewModelID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }

        // List to store selected TreatmentCodes
        [Required(ErrorMessage = "Required")]
        public List<int> TreatmentCodeIDs { get; set; } = new List<int>();

        [Required(ErrorMessage = "Required")]
        public int? UserId { get; set; } // Change this to string

        [DisplayName("Status")]
        public string? Status { get; set; } = "Admitted ";
    }


}
