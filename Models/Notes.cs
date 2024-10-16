using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class Notes
	{
        [Key]
        public int NotesID{ get; set; }
        public int MedicationNoteID { get; set; }
        public int OrderMedicationID { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderMedication OrderMedication { get; set; }

    }
}
