using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models.Admin
{
    public class AnConditions
    {
        [Key]
        public int AnConditionsID { get; set; }

        [DisplayName("Diagnose")]
        public string? Diagnose { get; set; }

        public string chroniccode { get; set; }
       

        
    }
}
