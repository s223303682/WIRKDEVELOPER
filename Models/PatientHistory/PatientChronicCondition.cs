using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WIRKDEVELOPER.Models.Account;
using WIRKDEVELOPER.Models.Admin;

namespace WIRKDEVELOPER.Models.PatientHistory
{
    public class PatientChronicCondition
    {
        [Key]
        public int PatientChronicConditionId { get; set; }

        [ForeignKey("PatientId")]
        public int PatientID { get; set; }

        [ForeignKey("ChronicConditionId")]
        public int? AnConditionsID { get; set; } = null;
        public virtual AnConditions AnConditions { get; set; }

    }
}
