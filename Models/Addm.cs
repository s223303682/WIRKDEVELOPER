using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WIRKDEVELOPER.Models.Admin;
using WIRKDEVELOPER.Models.PatientHistory;

namespace WIRKDEVELOPER.Models
{
    public class Addm
    {
        [Key]
        public int AddmID { get; set; }

        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        [DisplayName("Patient Name")]
        public virtual Patient Patient { get; set; }

        

        public int AnCurrentMedicationID { get; set; }
        [ForeignKey("AnCurrentMedicationID")]
        [DisplayName("Current Medication")]
        public virtual AnCurrentMedication AnCurrentMedication { get; set; }

        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentID")]
        [DisplayName("Treatment Code")]
        public virtual TreatmentCode TreatmentCode { get; set; }

        public int BedID { get; set; }
        [ForeignKey("BedID")]
        public virtual Bed Bed { get; set; }

        public int AnConditionsID { get; set; }
        [ForeignKey("AnConditionsID")]
        public virtual AnConditions AnConditions { get; set; }

        public int WardID { get; set; }
        [ForeignKey("WardID")]
        public virtual Ward Ward { get; set; }

        public int AnAllergiesID { get; set; }
        [ForeignKey("AnAllergiesID")]
        public virtual AnAllergies AnAllergies { get; set; }

        public int ChronicMedicationID { get; set; }
        [ForeignKey("ChronicMedicationID")]
        public virtual ChronicMedication ChronicMedication { get; set; }

        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;

        [DisplayName("Status")]
        public string? Status { get; set; } = "Admitted";
        public virtual MedicationActive MedicationActive { get; set; }

    }       
}
