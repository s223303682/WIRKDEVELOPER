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
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        [DisplayName("Email address")]
        public string? EmailAddress { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public DateTime? Time { get; set; }

        [Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }

        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }
        [Required(ErrorMessage = "Required")]
        public int AnaesthesiologistID { get; set; }

        [ForeignKey("AnaesthesiologistID")]
        public virtual Anaesthesiologist Anaesthesiologist { get; set; }

        // Store treatment codes as a comma-separated string or implement a many-to-many relationship
        [Required(ErrorMessage = "Required")]
        public string TreatmentCodeIDs { get; set; }
    }
    public class BookingViewModel
    {
        [Key]
        public int BookingViewModelID { get; set; }
        public int BookingID { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? OperationTheatreName { get; set; }
        public List<string> TreatmentCodes { get; set; } = new List<string>();
        [Required(ErrorMessage = "Required")]
        public string? AnaName { get; set; }
    }

}

