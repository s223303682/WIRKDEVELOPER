using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models.Admin
{
    public class MedicationActive
    {
        public int MedicationActiveID { get; set; }

        [ForeignKey("ChronicMedicationID")]
        public int? ChronicMedicationID { get; set; }

        public virtual ChronicMedication ChronicMedication { get; set; }

        [ForeignKey("ActiveIngredientId")]
        public int? ActiveID { get; set; }
        public virtual Active Active { get; set; }
    }
}
