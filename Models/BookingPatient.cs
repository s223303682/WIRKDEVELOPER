using Microsoft.Build.ObjectModelRemoting;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;
namespace WIRKDEVELOPER.Models
{
    public class BookingPatient
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        public string? PatientName { get; set; }
        [Required]
        public string? PatientSurname { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
       public string? email { get; set; }
        [Required]
        public DateTime ? Date { get; set; }
        [Required]
        public DateTime? Time { get; set; }
        //[Required]
        //public int?Theatre { get; set; }
        //[ForeignKey("TheatreID")]
        //public virtual Theatre? Theatre { get; set; }

        //public string? AnasthesiologistID { get; set; }
        //[ForeignKey("AnasthesiologistID")]
        //public virtual Anasthesiologist? Anasthesiologist { get; set; } 
    }
}
