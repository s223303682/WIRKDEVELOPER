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
        [Required]
        [DisplayName("ActiveIngridients")]
        public int? ActiveID { get; set; }
        [ForeignKey("ActiveID")]
        public virtual Active Active { get; set; }

        //[Required]
        //[DisplayName("Patient")]
        //public int? AddmID { get; set; }
        //[ForeignKey("AddmID")]
        //public virtual Patient Addm { get; set; }
        //[DisplayName("Reaction")]
        //public string? Reaction { get; set; }
        //[ForeignKey("AdmissionID")]

        //[DisplayName("Status")]
        //public string? Status { get; set; }
        //[ForeignKey("AddmID")]
        //      public virtual Addm Addm1 { get; set; }

        [Key]
		public int AllergiesID { get; set; }
	}
}
