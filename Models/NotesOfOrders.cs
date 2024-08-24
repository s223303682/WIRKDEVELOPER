using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class NotesOfOrders
    {
        [Key]
        public int NotesOfOrdersID { get; set; }


        [Required]
        [DisplayName("Patient")]
        public int? AddmID { get; set; }
        [ForeignKey("AddmID")]
        public virtual Patient Addm { get; set; }
    }
}
