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


        public int OrderMedicationID { get; set; }
        public string PharmacyMedicationName { get; set; }
        public string PatientName { get; set; }
        public string NoteText { get; set; }
    }
}
