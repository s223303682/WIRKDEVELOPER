using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        //public int PatientGender { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Gender")]

        public int CurrentMedicationID { get; set; }
        [ForeignKey("CurrentMedicationID")]
        [DisplayName("Current Medication")]
        public virtual CurrentMedication CurrentMedication { get; set; }

        public int TreatmentCodeID { get; set; }
        [ForeignKey("TreatmentID")]
        [DisplayName("Treatment Code")]
        public virtual TreatmentCode TreatmentCode { get; set; }

        //public int PatientPhone { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Phone")]

        //public int PatientEmail { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Email")]

        //public int PatientProvince { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Province")]

        //public string Address1 { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Address 1")]

        //public string Address2 { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("Address 2")]

        //public string City { get; set; }
        //[ForeignKey("PatientID")]
        //[DisplayName("City")]

        //public string Surbub { get; set; }
        //[ForeignKey("PatientID")]
        public int BedID { get; set; }
        [ForeignKey("BedID")]
        public virtual Bed Bed { get; set; }

        public int ConditionID { get; set; }
        [ForeignKey("ConditionID")]
        public virtual Condition Condition { get; set; }

        public int WardID { get; set; }
        [ForeignKey("WardID")]
        public virtual Ward Ward { get; set; }

        public int AllergiesID { get; set; }
        [ForeignKey("AllergiesID")]
        public virtual AnAllergies AnAllergies { get; set; }

        public int MedicationID { get; set; }
        [ForeignKey("MedicationID")]

        public int TreatmentID { get; set; }
        [ForeignKey("TreatmentID")]

        public string PostalCode { get; set; }
        [Required]
        [DisplayName(" Date")]
        public DateTime? Date { get; set; } = DateTime.Now;

        [DisplayName("Status")]
        public string? Status { get; set; } = "Admitted";
    }       
}
