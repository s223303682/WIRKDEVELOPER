using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class AnAllergies
    {

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; }

        [ForeignKey(nameof(Active))]
        public int ActiveId { get; set; } 
        public int ActiveID { get; set; }
        public virtual ActiveIngredient? Active { get; set; }


        [Key]
        public int AllergiesID { get; set; }
    }
}
