using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
    public class Vitals
    {
        [Key]
        public int VitalID { get; set; }

        [Required]
        [DisplayName("Vital Name")]
        public string? VitalType { get; set; }
        [Required]
        [DisplayName("Minimum Range")]
        public string? Minimumrange { get; set; }

        [Required]
        [DisplayName("Maximum Range ")]
        public string? Maximumrange { get; set; }
    }
}
