using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Account;
namespace WIRKDEVELOPER.Models
{
    public class BookingNew
    {

        [Key]
        public int BookingNewID { get; set; }

        //[Required(ErrorMessage = "Required")]
        //public int? PatientID { get; set; }
        //[ForeignKey("PatientID")]
        //public virtual Patient? patient { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Name")]
        public string? BookingPatientName { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Surname")]
        public string? BookingPatientSurname { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("ID Number")]
        [MaxLength(13)]
        public string? BookingPatientIDNUmber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? country { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? province { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Surbub { get; set; } 
        [Required(ErrorMessage = "Required")]
        public int? Zip { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Contact Number")]
        [MaxLength(10)]
        public string? ContactNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Required")]
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
        public virtual TreatmentCode? treatmentCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public int UserId { get; set; }
        [ForeignKey("AnaestesiologistID")]
        public virtual Anaesthesiologist? Anaestesiologist { get; set; }
    }
}
