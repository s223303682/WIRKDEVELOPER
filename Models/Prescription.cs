using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Areas.Identity.Data;

namespace WIRKDEVELOPER.Models
{
    public class Prescription
    {

        [Key]
        public int PrescriptionID { get; set; }
        public int? PatientID { get; set; }
        [Required(ErrorMessage = "Required")]
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }
        [Required]
        [DisplayName("Prescription Date")]
        public DateTime? Date { get; set; }
        public string? prescriber { get; set; }
        [ForeignKey("ID")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
        [Required]
        public string? Medication { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [Required]
        [DisplayName("Instructions")]
        public string? Instructions { get; set; }
        [Required]
        public string? Urgent { get; set; }
        [Required]
        public string? status { get; set; }




    }
}
