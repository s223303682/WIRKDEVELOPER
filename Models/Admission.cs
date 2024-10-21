using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Account;

namespace WIRKDEVELOPER.Models
{
    public class Admission
    {
        [Key]
        public int AdmissionID { get; set; }
        [ForeignKey("Booking")]
        public int BookingNewPatientID { get; set; }
        public virtual BookingNewPatient? BookingNewPatient { get; set; }

        [ForeignKey("Bed")]
        public int BedId { get; set; }
        public virtual Bed? Bed { get; set; }

        [ForeignKey("Nurse")]
        public int NurseId { get; set; }
        public virtual Nurse? Nurse { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]

        public double Weight { get; set; }
        public string Status { get; set; } = "Admitted";

        [Required]
        public DateTime Date { get; set; }
        [NotMapped]
        public int Pat { get; set; }
    }
    public class Discharge
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Admission")]
        public int AdmissionId { get; set; }
        public virtual Admission? Admission { get; set; }

        [Display(Name = "Notes"), StringLength(150)]
        public string NurseNotes { get; set; }
        public DateTime Date { get; set; }

    }

}
