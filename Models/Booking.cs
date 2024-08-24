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
        [Required(ErrorMessage = "Required")]
        public int? AddmID { get; set; }
        [ForeignKey("AddmID")]
        public virtual Addm? Addm { get; set; }
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
        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentCodeID")]
        public virtual TreatmentCode? TreatmentCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Anaestesiologist { get; set; }
        
    }
}
