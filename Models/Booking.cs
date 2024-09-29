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
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
       
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Required")]
        public int? OperationTheatreID { get; set; }
        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? userId { get; set; }
        [ForeignKey("userId")]
        public virtual Anaesthesiologist? Anaesthesiologist { get; set; }
        [Required(ErrorMessage = "Required")]
        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentCodeID")]
        public virtual TreatmentCode? treatmentCode { get; set; }
   
        [DisplayName("Status")]
        public string? Status { get; set; } = "Not Admitted ";
    }

}
