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
        public string Gender { get; set; }

        public string Email { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }


        [Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }
        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }
        [Required(ErrorMessage = "Required")]
        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentCodeID")]
        public virtual TreatmentCode? treatmentCode { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Anaesthesiologist? Anaesthesiologist { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; } = "Admitted ";
    }

}
