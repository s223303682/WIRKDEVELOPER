﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WIRKDEVELOPER.Models.Account;


namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string IDNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Prescriber { get; set; }
        [Required]
        public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; }
        [Required]
        [DisplayName("Prescription Date")]
        //public DateTime Date { get; set; }
        [ForeignKey("Surgeon")]
        public int SurgeonID { get; set; }
        public virtual Surgeon? Surgeon { get; set; }
        [ForeignKey("Nurse")]
        public int? NurseID { get; set; }
        public virtual Nurse? Nurse { get; set; }

        public string Urgent { get; set; }
        public string Status { get; set; } = "Prescribed";
        public string? IgnoreReason { get; set; } = "none";
        // Navigation propertiesnine
        public virtual List<PrescriptionMedication> PrescriptionMedications { get; set; } = new List<PrescriptionMedication>();
     
    }

    public class PrescriptionViewModel
    {
        [Key]
        public int PrescriptionViewModelID { get; set; }

        public int PrescriptionID { get; set; }// Consider renaming this to avoid confusion with PrescriptionID
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string IDNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Prescriber { get; set; }
        [Required]
        public string Urgent { get; set; }
        [Required]
        public string Status { get; set; }
        public string IgnoreReason { get; set; }

        // List of medications to be added dynamically
        public List<PrescriptionMedicationViewModel> Medications { get; set; } = new List<PrescriptionMedicationViewModel>();
        public bool HasAlerts { get; set; } // Add this property
    }

    public class PrescriptionMedication
    {
        [Key]
        public int PrescriptionMedicationID { get; set; }
        public int PrescriptionID { get; set; }
        public int PharmacyMedicationID { get; set; }
        [Required, Range(1, int.MaxValue)]

        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        public int QuantityGiven { get; set; } = 0;
        public string Instructions { get; set; }

        // Navigation properties
        public virtual Prescription Prescription { get; set; }
        public virtual PharmacyMedication PharmacyMedication { get; set; }
    }

    public class PrescriptionMedicationViewModel
    {
        [Key]
        public int PrescriptionMedicationViewModelID { get; set; }
        [Required]
        public string PharmacyMedicationName { get; set; } // Change from ID to Name
        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public int QuantityGiven { get; set; } = 0;
        public string Instructions { get; set; }
    }
    public class AlertViewModel
    {
        public bool HasAlerts { get; set; }
        public string IgnoreReason { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IDNumber { get; set; }
        public DateTime Date { get; set; }
        public string Prescriber { get; set; }
        public string Status { get; set; }
        public string Urgent { get; set; }

        // Add the Medications property here
        public List<PrescriptionMedicationViewModel> Medications { get; set; }
    }


    public class AlertMedication
    {
        [Key]
        public int AlertMedicationID { get; set; }
        public string PharmacyMedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }


}

