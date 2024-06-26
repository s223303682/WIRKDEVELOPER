﻿using System;
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
        [DisplayName("Patient name")]
        public string? PatientName { get; set; }
        [Required]
        [DisplayName("Patient Surname")]
        public string? PatientPatient { get; set; }
        [Required]
        public string? address { get; set; }
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

        public int? AdminID { get; set; }
        [ForeignKey("AdminID")]
        public virtual Admin? admin { get; set; }


    }
}
