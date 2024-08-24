using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {

        [Key]
        public int PrescriptionID { get; set; } // Unique identifier for the prescription

        [Required]
        public string BookingID { get; set; } // ID of the related booking

        [Required]
        [Display(Name = "Patient Name")]
        public string Name { get; set; } // Name of the patient

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; } // Gender of the patient

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } // Email of the patient

        [Required]
        [DisplayName("Prescription Date")]
        public DateTime? Date { get; set; } // Date when the prescription was created

        [Display(Name = "Prescriber")]
        public string? Prescriber { get; set; } // Name of the person or system who created the prescription

        [Required]
        [Display(Name = "Urgent")]
        public string? Urgent { get; set; } // Indicates if the prescription is urgent (e.g., "Yes", "No")

        [Required]
        [Display(Name = "Status")]
        public string? Status { get; set; }




    }
}
