using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WIRKDEVELOPER.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        [DisplayName("Patient ID Number")]
        public string? PatientIDNO { get; set; }
        [Required]
        [DisplayName("Patient Name")]
        public string? PatientName { get; set; }
        [Required]
        [DisplayName("Patient Surname")]
        public string? PatientSurname { get; set; }
        [Required]
        public string? province { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? surbub { get; set; }
        [Required]
        public int? zip { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public int? contactNumber { get; set; }
        [Required]
        [DisplayName("Email address")]
        public string? EmailAddress { get; set; }
        [Required]
        [DisplayName("Address 1")]
        public string? Address1 { get; set; }

        [Required]
        [DisplayName("Address 2")]
        public string? Address2 { get; set; }

        [Required]
        [DisplayName("Ward Name ")]
        public string? wardName { get; set; }
        [ForeignKey("WardID")]

        [Required]
        [DisplayName("Condition ")]
        public string? Condition { get; set; }
        [ForeignKey("ConditionID")]

        [Required]
        [DisplayName("Medication Name")]
        public string? MedicationName { get; set; }
        [ForeignKey("MedicationID")]

        [Required]
        [DisplayName("Treatment Code")]
        public string? TreatmentCode { get; set; }
        [ForeignKey("TreatmentCodeID")]

        [Required]
        [DisplayName("Allergies")]
        public string? AllergiesName { get; set; }
        [ForeignKey("AllergiesID")]

        [Required]
        [DisplayName("Date of birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string? Gender { get; set; }




    }
}
