using Microsoft.AspNetCore.Mvc.Rendering;
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
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Anaesthesiologist Anaesthesiologist { get; set; }

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
        public string? AnaName { get; set; }
        public List<string> TreatmentCodes { get; set; } = new List<string>();
        
      
    }
    public class CreateBookingViewModel
    {
        // Booking properties
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime? Time { get; set; }

        [Required]
        [Display(Name = "Operation Theatre")]
        public int? OperationTheatreID { get; set; }

        [Required]
        [Display(Name = "Anaesthesiologist")]
        public int? UserId { get; set; }

        [Required]
        [Display(Name = "Treatment Codes")]
        public List<string> TreatmentCodeIDs { get; set; } = new List<string>();

        // Dropdown lists
        public IEnumerable<SelectListItem> OperationTheatres { get; set; }
        public IEnumerable<SelectListItem> TreatmentCodes { get; set; }
        public IEnumerable<SelectListItem> Anaesthesiologists { get; set; }
    }

}

