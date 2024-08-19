
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;
using WIRKDEVELOPER.Areas.Identity.Data;
namespace WIRKDEVELOPER.Models
{
    public class BookingPatient
    {
        [Key]
        public int BookingPatientID { get; set; }
        
        public int Total { get; set; }
        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual ApplicationUser? mainUser { get; set; }

        [Required]
        public string? BookingPatientName { get; set; }
        [Required]
        public string? BookingPatientSurname { get; set; }
        [Required]
        public string? country { get; set; }
        [Required]
        public string? province { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Surbub { get; set; }
        [Required]
        public int? Zip { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string? ContactNumber { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
       public string? email { get; set; }
        [Required]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime ? Date { get; set; }
        [Required]
        public DateTime? Time { get; set; }
        [Required]
        public int? OperationTheatreID { get; set; }
        [ForeignKey("OperationTheatreID")]
        public virtual OperationTheatre? OperationTheatre { get; set; }
        public int TreatmentID { get; set; }
        [ForeignKey("TreatmentID")]
        public virtual Treatment? Treatment { get; set; }
        public int MedicalProfessionalID { get; set; }
        [ForeignKey("MedicalProfessionalID")]
        public virtual MedicalProfessional? MedicalProfessional { get; set; }
    }
}
