﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? patient { get; set; }


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
        [DisplayName("Treatment Code")]

        public int TreatmentID { get; set; }
        [ForeignKey("TreatmentID")]
        public virtual Treatment? Treatment { get; set; }
        [Required(ErrorMessage = "Required")]

        public int MedicalProfessionalID { get; set; }
        [ForeignKey("MedicalProfessionalID")]
        public virtual MedicalProfessional? MedicalProfessionalS { get; set; }

    }
}
