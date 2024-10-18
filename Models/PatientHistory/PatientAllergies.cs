using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Account;


namespace WIRKDEVELOPER.Models.PatientHistory
{
    public class PatientAllergies
    {
        [Key]
        public int PatientAllergiesId { get; set; }

        [ForeignKey("PatientId")]
        public int PatientID { get; set; }

        [ForeignKey("ActiveIngredientId")]
        public int? ActiveID { get; set; }
        public virtual Active Active { get; set; }
    }
}
