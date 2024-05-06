using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class PatientCondition
    {
        [Key]
        public int PatientConditionID { get; set; }
        public int ? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient? Patient { get; set; }
        public int? ConditionID { get; set; }
        [ForeignKey("ConditionID")]
        public virtual Condition? Condition { get; set; }

    }
}
