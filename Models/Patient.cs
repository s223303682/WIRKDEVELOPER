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
        [DisplayName("Patient name")]
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
        public int? contactNumber {  get; set; }
        [Required]
        [DisplayName("Email address")]
        public string? EmailAddress { get; set;}
        [Required]
        [DisplayName("Date of birth")]
        public DateTime? DateOfBirth { get; set;}
        [Required]
        public string? Gender { get; set; }

       


    }
}
