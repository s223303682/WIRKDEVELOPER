using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models.Admin
{
    public class AnAllergies
    {
        [Key]
        public int AnAllergiesID { get; set; }

        [Required]
        [DisplayName("ActiveIngridients")]
        public int? ActiveID { get; set; }
        [ForeignKey("ActiveID")]
        public virtual Active Active { get; set; }

        
    }
}
